using HrLink.Application.Common.Results;
using HrLink.Application.DTOs;

namespace HrLink.Application.UseCases.AuthUseCase.Login;

public interface ILoginUseCase
{
    Task<Result<LoginDataResponse>> Execute(LoginCommand command, CancellationToken cancellationToken);
}