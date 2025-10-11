using HrLink.Application.Common.Results;
using HrLink.Application.Common.Results.Errors;
using HrLink.Application.Interfaces;
using HrLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HrLink.Application.UseCases.InterviewUseCases.ChangeInterviewStatus;

public class ChangeInterviewStatusUseCase : IChangeInterviewStatusUseCase
{
    private readonly IApplicationDbContext _context;

    public ChangeInterviewStatusUseCase(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Result> Execute(ChangeInterviewStatusCommand command, CancellationToken cancellationToken)
    {
        if (!await _context.Interviews.AnyAsync(x => x.Id == command.InterviewId, cancellationToken))
        {
            return Result.Failure(new NotFoundError<Interview>(nameof(command.StatusId)));
        }
        
        if (!await _context.Statuses.AnyAsync(x => x.Id == command.StatusId, cancellationToken))
        {
            return Result.Failure(new NotFoundError<Status>( nameof(command.StatusId)));
        }

        await _context.Interviews.Where(x=> x.Id == command.InterviewId).ExecuteUpdateAsync(x => 
                x.SetProperty(i => i.StatusId, command.StatusId), cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}