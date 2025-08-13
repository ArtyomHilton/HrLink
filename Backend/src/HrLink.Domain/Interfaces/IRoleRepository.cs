using HrLink.Domain.Entities;

namespace HrLink.Domain.Interfaces;

/// <summary>
/// Репозиторий для <see cref="Role"/>.
/// </summary>
public interface IRoleRepository
{
    /// <summary>
    /// Добавляет <see cref="Role"/> в БД.
    /// </summary>
    /// <param name="role">Данные <see cref="Role"/>.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    /// <returns>Добавленную <see cref="Role"/>.</returns>
    Task<Role> AddRoleAsync(Role role, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получает все <see cref="Role"/> из БД.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    /// <returns>Список всех <see cref="Role"/>.</returns>
    Task<List<Role>> GetRolesAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Получение роли по <paramref name="id"/>.
    /// </summary>
    /// <param name="id">Идентификатор <see cref="Role"/>.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    /// <returns>Полученную <see cref="Role"/> или <see cref="Nullable"/></returns>
    Task<Role?> GetRoleByIdAsync(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Обновление <see cref="Role"/> по id.
    /// </summary>
    /// <param name="role">Данные для обновления роли.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    /// <returns>Количество обновленных строк</returns>
    Task<int?> UpdateRoleByIdAsync(Role role, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удаление <see cref="Role"/> по <paramref name="id"/>.
    /// </summary>
    /// <param name="id">Идентификатор роли.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    /// <returns>Количество удаленных записей.</returns>
    Task<int?> DeleteRoleById(Guid id, CancellationToken cancellationToken);
}