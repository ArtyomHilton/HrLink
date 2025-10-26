using FluentValidation;
using HrLink.Application.Common.Results;
using HrLink.Application.Common.Results.Errors;
using HrLink.Application.DTOs;
using HrLink.Application.Interfaces;
using HrLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HrLink.Application.UseCases.UserUseCases.GetUserByIdUser;

public class GetUserByIdUseCase : IGetUserByIdUseCase
{
    private readonly IApplicationDbContext _context;
    private readonly ICacheService _cacheService;
    private readonly IValidator<GetUserByIdQuery> _validator;

    public GetUserByIdUseCase(IApplicationDbContext context, ICacheService cacheService,
        IValidator<GetUserByIdQuery> validator)
    {
        _context = context;
        _cacheService = cacheService;
        _validator = validator;
    }

    public async Task<Result<UserDetailDataResponse>> Execute(GetUserByIdQuery query,
        CancellationToken cancellationToken)
    {
        var validateResult = await _validator.ValidateAsync(query, cancellationToken);

        if (!validateResult.IsValid)
        {
            return Result.Failure<UserDetailDataResponse>(
                new ValidateError(validateResult.Errors[0].ErrorCode, validateResult.Errors[0].PropertyName));
        }

        var user = await _cacheService.GetAsync<UserDetailDataResponse?>($"user_{query.Id}", cancellationToken);

        if (user is not null)
        {
            return Result.Success<UserDetailDataResponse>(user);
        }

        user = await _context.Users
            .Where(x => x.Id == query.Id && !x.IsDelete)
            .Select(x =>
                new UserDetailDataResponse(
                    x.Id,
                    x.FirstName,
                    x.SecondName,
                    x.Patronymic,
                    x.DateOfBirthday,
                    x.Email,
                    x.Employee == null
                        ? null
                        : new EmployeeDetailDataResponse(
                            x.Employee.Id,
                            x.Employee.Position,
                            x.Employee.WorkEmail,
                            x.Employee.WorkPhoneNumber,
                            x.Employee.DateOfEmployment,
                            x.Employee.Interviews.Select(i => new InterviewShortDataResponse(
                                        i.Id,
                                        i.Vacancy.Position,
                                        i.InterviewDateTime,
                                        i.Status.StatusName))
                                    .ToList())))
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserDetailDataResponse>(new NotFoundError<User>(nameof(query.Id),
                new Dictionary<string, object?>() { ["UserId"] = query.Id }));
        }

        await _cacheService.SetAsync($"user_{query.Id}", user, cancellationToken);

        return Result.Success<UserDetailDataResponse>(user);
    }
}