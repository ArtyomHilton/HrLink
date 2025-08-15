using HrLink.Domain.Entities;

namespace HrLink.Domain.Interfaces.RoleRepositories;

public interface IGetAllRolesRepository
{
    /// <summary>
    /// Получает все <see cref="Role"/> из БД.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    /// <returns>Список всех <see cref="Role"/>.</returns>
    Task<List<Entities.Role>> GetRolesAsync(CancellationToken cancellationToken);
}