using HrLink.Application.Common.Results;

namespace HrLink.Application.UseCases.InterviewUseCases.ChangeInterviewStatus;

public interface IChangeInterviewStatusUseCase
{
    Task<Result> Execute(ChangeInterviewStatusCommand command, CancellationToken cancellationToken);
}