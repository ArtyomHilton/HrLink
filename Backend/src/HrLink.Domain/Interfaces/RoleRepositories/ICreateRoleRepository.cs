using HrLink.Domain.Entities;

namespace HrLink.Domain.Interfaces.RoleRepositories;

public interface ICreateRoleRepository
{
    /// <summary>
    /// Добавляет <see cref="Role"/> в БД.
    /// </summary>
    /// <param name="role">Данные <see cref="Role"/>.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    /// <returns>Добавленную <see cref="Role"/>.</returns>
    Task<Role> AddRoleAsync(Role role, CancellationToken cancellationToken);

    /// <summary>
    /// Проверяет существует ли роль с названием <paramref name="name"/>. 
    /// </summary>
    /// <param name="name">Название роли</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Наличие роли в БД.</returns>
    Task<bool> RoleNameExist(string name, CancellationToken cancellationToken);
}