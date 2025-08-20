using HrLink.Application.Common.Results;
using HrLink.Domain.Entities;

namespace HrLink.Application.UseCases.UserUseCases.AddUser;

public interface IAddUserUseCase
{
    Task<Result<User?>> Execute(User user, CancellationToken cancellationToken);
}