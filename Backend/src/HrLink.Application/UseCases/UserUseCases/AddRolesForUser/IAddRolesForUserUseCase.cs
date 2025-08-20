using HrLink.Application.Common.Results;
using HrLink.Domain.Entities;

namespace HrLink.Application.UseCases.UserUseCases.AddRolesForUser;

public interface IAddRolesForUserUseCase
{
    public Task<Result> Execute(AddRolesForUserCommand command, CancellationToken cancellationToken);
}