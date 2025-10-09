using FluentValidation;
using HrLink.Application.Common.Results;
using HrLink.Application.Common.Results.Errors;
using HrLink.Application.Interfaces;
using HrLink.Application.Validators;
using HrLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HrLink.Application.UseCases.InterviewUseCases.AddInterview;

public class AddInterviewUseCase : IAddInterviewUseCase
{
    private readonly IApplicationDbContext _context;
    private readonly int _avgDurationInterview = 1;
    private readonly IValidator<AddInterviewCommand> _validator;

    public AddInterviewUseCase(IApplicationDbContext context, IValidator<AddInterviewCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<Result<Interview?>> Execute(AddInterviewCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Result.Failure<Interview?>(null,
                new ValidateError(validationResult.Errors[0].ErrorCode, validationResult.Errors[0].PropertyName));
        }

        if (!await _context.Vacancies.AnyAsync(x => x.Id == command.VacancyId, cancellationToken))
        {
            return Result.Failure<Interview?>(null, new ValidateError("VacancyNotExist", nameof(command.VacancyId)));
        }

        if (!await _context.Candidates.AnyAsync(x => x.Id == command.CandidateId, cancellationToken))
        {
            return Result.Failure<Interview?>(null,
                new ValidateError("CandidateNotExist", nameof(command.CandidateId)));
        }

        if (!await _context.Employees.AnyAsync(x => x.Id == command.EmployeeId, cancellationToken))
        {
            return Result.Failure<Interview?>(null, new ValidateError("EmployeeNotExist", nameof(command.EmployeeId)));
        }

        var interview = await _context.Interviews
            .Where(x => x.CandidateId == command.CandidateId
                        || x.EmployeeId == command.EmployeeId
                        && (x.InterviewDateTime >= command.InterviewDateTime.AddHours(-_avgDurationInterview)
                            || x.InterviewDateTime <= command.InterviewDateTime.AddHours(_avgDurationInterview)))
            .FirstOrDefaultAsync(cancellationToken);

        if (interview is not null)
        {
            return Result.Failure<Interview?>(null,
                new ValidateError("InterviewSchedulingConflict", nameof(command.InterviewDateTime),
                    new Dictionary<string, object?>()
                    {
                        ["CurrentInterviewDateTime"] = command.InterviewDateTime,
                        ["StartInterviewDateTime"] = command.InterviewDateTime.AddHours(-_avgDurationInterview),
                        ["EndInterviewDateTime"] = command.InterviewDateTime.AddHours(_avgDurationInterview)
                    }));
        }

        var entry = await _context.Interviews.AddAsync(command.ToEntity(), cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success<Interview?>(entry.Entity);
    }
}