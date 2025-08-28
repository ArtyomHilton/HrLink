using HrLink.Application.Common.Results;
using HrLink.Domain.Entities;

namespace HrLink.Application.UseCases.UserUseCases.GetUserByIdUser;

public interface IGetUserByIdUseCase
{
    public Task<Result<User?>> Execute(Guid id, CancellationToken cancellationToken);
}