using HrLink.Application.Common.Results;
using HrLink.Application.Common.Results.Errors;
using HrLink.Domain.Entities;
using HrLink.Domain.Interfaces.UserRepositories;

namespace HrLink.Application.UseCases.UserUseCases.AddUser;

public class AddUserUseCase : IAddUserUseCase
{
    private readonly IAddUserRepository _repository;

    public AddUserUseCase(IAddUserRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Result<User?>> Execute(AddUserCommand command, CancellationToken cancellationToken)
    {
        if (await _repository.EmailExistAsync(command.Email, cancellationToken))
        {
            return Result.Failure<User?>(null, new UserEmailExistError("Данные Email уже занят", nameof(command.Email)));
        }

        command.RoleIds = await _repository.GetExistRolesByIdsAsync(command.RoleIds, cancellationToken);

        var entity = await _repository.AddUserAsync(command.ToModel(), cancellationToken);

        return entity is null 
            ? Result.Failure<User?>(entity ,new UserEmailExistError("Данные Email уже занят", nameof(command.Email))) 
            : Result.Success<User?>(entity);
    }
}