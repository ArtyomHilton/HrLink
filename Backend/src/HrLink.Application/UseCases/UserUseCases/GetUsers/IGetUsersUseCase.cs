using HrLink.Application.Common.Results;
using HrLink.Domain.Entities;

namespace HrLink.Application.UseCases.UserUseCases.GetUsers;

public interface IGetUsersUseCase
{
    Task<Result<List<User>?>> Execute(GetUsersQuery query, CancellationToken cancellationToken);
}