using HrLink.Application.Common.Results;

namespace HrLink.Application.UseCases.UserUseCases.ChangePassword;

public interface IChangeUserPasswordUseCase
{
    Task<Result> Execute(ChangeUserPasswordCommand command, CancellationToken cancellationToken);
}