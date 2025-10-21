using FluentValidation;
using HrLink.Application.Common.Results;
using HrLink.Application.Common.Results.Errors;
using HrLink.Application.DTOs;
using HrLink.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HrLink.Application.UseCases.InterviewUseCases.GetInterviewByDay;

public class GetInterviewsByDateUseCase : IGetInterviewsByDateUseCase
{
    private readonly IApplicationDbContext _context;
    private readonly ICacheService _cacheService;
    private readonly IValidator<GetInterviewsByDateQuery> _validator;
    
    public GetInterviewsByDateUseCase(IApplicationDbContext context, ICacheService cacheService, IValidator<GetInterviewsByDateQuery> validator)
    {
        _context = context;
        _cacheService = cacheService;
        _validator = validator;
    }
    
    public async Task<Result<List<GetInterviewByDateDataResponse>>> Execute(GetInterviewsByDateQuery query, CancellationToken cancellationToken)
    {
        var validateResult = await _validator.ValidateAsync(query, cancellationToken);

        if (!validateResult.IsValid)
        {
            return Result.Failure<List<GetInterviewByDateDataResponse>>([] ,new ValidateError(validateResult.Errors[0].ErrorCode,
                validateResult.Errors[0].PropertyName));
        }
        
        var key = $"interviews_{query.InterviewDate:yyyy-MM-dd}";

        var interviews = await _cacheService.GetAsync<List<GetInterviewByDateDataResponse>>(key, cancellationToken);

        if (interviews is not null && interviews.Any())
        {
            return Result.Success(interviews);
        }

        var startDate = DateTime.SpecifyKind(query.InterviewDate.Date, DateTimeKind.Utc);
        var endDate = startDate.AddDays(1);
        
        Console.WriteLine(startDate);
        
        interviews = await _context.Interviews
            .Where(x=> x.InterviewDateTime >=  startDate
                       && x.InterviewDateTime < endDate)
            .Select(x => new GetInterviewByDateDataResponse(
            new InterviewShortDataResponse(
                x.Id,
                x.Vacancy.Position,
                x.InterviewDateTime,
                x.Status.StatusName),
            new CandidateShortDateResponse(
                x.Candidate.Id,
                x.Candidate.FirstName,
                x.Candidate.SecondName,
                x.Candidate.Patronymic,
                x.Candidate.Email)))
            .ToListAsync(cancellationToken);

        if (interviews.Any())
        {
            await _cacheService.SetAsync(key, interviews, cancellationToken);
        }
        
        return Result.Success(interviews);
    }
}