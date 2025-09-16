using HrLink.Application.Common.Results;
using HrLink.Application.Common.Results.Errors;
using HrLink.Application.Interfaces;
using HrLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HrLink.Application.UseCases.InterviewUseCases.AddInterview;

public class AddInterviewUseCase : IAddInterviewUseCase
{
    private readonly IApplicationDbContext _context;
    private readonly int _avgDurationInterview = 1;

    public AddInterviewUseCase(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Interview?>> Execute(AddInterviewCommand command, CancellationToken cancellationToken)
    {
        if (!await _context.Vacancies.AnyAsync(x => x.Id == command.VacancyId, cancellationToken))
        {
            return Result.Failure<Interview?>(null, new NotExistError<Vacancy>(command.VacancyId));
        }

        if (!await _context.Candidates.AnyAsync(x => x.Id == command.CandidateId, cancellationToken))
        {
            return Result.Failure<Interview?>(null, new NotExistError<Candidate>(command.CandidateId));
        }

        if (!await _context.Employees.AnyAsync(x => x.Id == command.EmployeeId, cancellationToken))
        {
            return Result.Failure<Interview?>(null, new NotExistError<Employee>(command.EmployeeId));
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
                new InterviewSchedulingConflictError(command.InterviewDateTime.AddHours(-1),
                    command.InterviewDateTime.AddHours(1)));
        }

        var entry = await _context.Interviews.AddAsync(command.ToEntity(), cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success<Interview?>(entry.Entity);
    }
}