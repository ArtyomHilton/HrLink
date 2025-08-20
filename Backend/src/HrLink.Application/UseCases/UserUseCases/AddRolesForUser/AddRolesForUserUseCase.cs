using HrLink.Application.Common.Results;
using HrLink.Application.Common.Results.Errors;
using HrLink.Domain.Entities;
using HrLink.Domain.Interfaces.UserRepositories;

namespace HrLink.Application.UseCases.UserUseCases.AddRolesForUser;

public class AddRolesForUserUseCase : IAddRolesForUserUseCase
{
    private readonly IAddRolesForUserRepository _repository;

    public AddRolesForUserUseCase(IAddRolesForUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Execute(AddRolesForUserCommand command, CancellationToken cancellationToken)
    {
        command.RoleIds = await _repository.GetExistRolesByIdsAsync(command.RoleIds, cancellationToken);

        if (!command.RoleIds!.Any())
            return Result.Failure(new NoRolesError("Указанные вами роли не существуют", nameof(command.RoleIds)));

        command.RoleIds =
            await _repository.GetMissedRolesByUserIdFromListRoleIds(command.UserId, command.RoleIds, cancellationToken);

        if (!command.RoleIds!.Any())
            return Result.Failure(new NoRolesError("Указанные вами роли уже присутствуют у этого пользователя",
                nameof(command.RoleIds)));

        return await _repository.AddRolesForUserByUserId(command.ToModel(), cancellationToken)
            ? Result.Success()
            : Result.Failure(new Error(nameof(command.RoleIds)));
    }
}