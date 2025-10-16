using HrLink.Application.Common.Results;
using HrLink.Application.DTOs;

namespace HrLink.Application.UseCases.UserUseCases.GetUsers;

/// <summary>
/// Обработчик запроса получения пользователей.
/// </summary>
public interface IGetUsersUseCase
{
    /// <summary>
    /// Выполняет запрос получения пользователей.
    /// Полчает пользователей.
    /// </summary>
    /// <param name="query"><see cref="GetUsersQuery"/> Запрос получения пользователей.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Результат выполнения запроса.</returns>
    Task<Result<List<UserShortDataResponse>>> Execute(GetUsersQuery query, CancellationToken cancellationToken);
}