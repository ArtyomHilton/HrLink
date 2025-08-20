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
    
    public async Task<Result<User?>> Execute(User user, CancellationToken cancellationToken)
    {
        if (await _repository.EmailExistAsync(user.Email, cancellationToken))
        {
            return Result.Failure<User?>(user, new UserEmailExistError("Данные Email уже занят", nameof(user.Email)));
        }

        var entity = await _repository.AddUserAsync(user, cancellationToken);

        return entity is null 
            ? Result.Failure<User?>(entity ,new UserEmailExistError("Данные Email уже занят", nameof(user.Email))) 
            : Result.Success<User?>(entity);
    }
}