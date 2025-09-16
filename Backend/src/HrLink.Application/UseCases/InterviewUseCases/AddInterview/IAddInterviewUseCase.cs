using HrLink.Application.Common.Results;
using HrLink.Domain.Entities;

namespace HrLink.Application.UseCases.InterviewUseCases.AddInterview;

public interface IAddInterviewUseCase
{
    Task<Result<Interview?>> Execute(AddInterviewCommand command, CancellationToken cancellationToken);
}