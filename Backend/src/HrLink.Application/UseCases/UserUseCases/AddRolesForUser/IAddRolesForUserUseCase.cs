using HrLink.Application.Common.Results;

namespace HrLink.Application.UseCases.UserUseCases.AddRolesForUser;

/// <summary>
/// Обработчик команды добавления ролей для пользователя.
/// </summary>
public interface IAddRolesForUserUseCase
{
    /// <summary>
    /// Выполняет команду <paramref name="command"/>
    /// Добавляет роли для пользователя.
    /// </summary>
    /// <param name="command"><see cref="AddRolesForUserCommand"/> Данные для добавления ролей пользователю.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Результат выполнения команды.</returns>
    public Task<Result> Execute(AddRolesForUserCommand command, CancellationToken cancellationToken);
}