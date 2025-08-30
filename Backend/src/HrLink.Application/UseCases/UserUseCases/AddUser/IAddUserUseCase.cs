using HrLink.Application.Common.Results;
using HrLink.Domain.Entities;

namespace HrLink.Application.UseCases.UserUseCases.AddUser;

/// <summary>
/// Обработчик команды добавления пользователя.
/// </summary>
public interface IAddUserUseCase
{
    /// <summary>
    /// Выполняет команду добавления пользователя <see cref="AddUserCommand"/>.
    /// Добавляет пользователя.
    /// </summary>
    /// <param name="command"><see cref="AddUserCommand"/> Данные для добавления пользователя.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Результат выполнения команды.</returns>
    Task<Result<User?>> Execute(AddUserCommand command, CancellationToken cancellationToken);
}