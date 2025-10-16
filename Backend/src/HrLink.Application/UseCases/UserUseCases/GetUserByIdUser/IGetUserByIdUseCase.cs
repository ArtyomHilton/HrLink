using HrLink.Application.Common.Results;
using HrLink.Application.DTOs;

namespace HrLink.Application.UseCases.UserUseCases.GetUserByIdUser;

/// <summary>
/// Обработчик запроса получения пользователя по идентификатору.
/// </summary>
public interface IGetUserByIdUseCase
{
    /// <summary>
    /// Выполняет запрос получения пользователя по идентификатору.
    /// Получает пользователя по идентификатору.
    /// </summary>
    /// <param name="query"><see cref="GetUserByIdQuery"/> Запрос для получения пользователя по идентификатору</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Результат выполнения запроса.</returns>
    public Task<Result<UserDetailDataResponse?>> Execute(GetUserByIdQuery query, CancellationToken cancellationToken);
}