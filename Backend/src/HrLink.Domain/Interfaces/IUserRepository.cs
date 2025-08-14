using HrLink.Domain.Entities;

namespace HrLink.Domain.Interfaces;

public interface IUserRepository
{
    /// <summary>
    /// Добавляет <see cref="User"/> в БД.
    /// </summary>
    /// <param name="user">Данные <see cref="User"/>.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    /// <returns>Добавленный <see cref="User"/>.</returns>
    Task<User> AddUserAsync(User user, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получает всех <see cref="User"/> из БД.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    /// <returns>Список всех <see cref="User"/>.</returns>
    Task<List<User>?> GetUsersAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Получение <see cref="User"/> по <paramref name="id"/>.
    /// </summary>
    /// <param name="id">Идентификатор <see cref="Role"/>.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    /// <returns>Полученный <see cref="User"/> или <see cref="Nullable"/></returns>
    Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Обновление <see cref="User"/> по id.
    /// </summary>
    /// <param name="user">Данные для обновления роли.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    /// <returns>Количество обновленных строк</returns>
    Task<int?> UpdateUserByIdAsync(User user, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удаление и восстановление <see cref="User"/> по <paramref name="id"/>.
    /// </summary>
    /// <param name="id">Идентификатор <see cref="User"/>.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    /// <returns>Количество удаленных записей.</returns>
    Task<int?> ChangeDeleteUserStatusByIdAsync(Guid id, CancellationToken cancellationToken);
}