using HrLink.Domain.Entities;

namespace HrLink.Domain.Interfaces.RoleRepositories;

public interface IUpdateRoleByIdRepository
{
    /// <summary>
    /// Обновление <see cref="Role"/> по id.
    /// </summary>
    /// <param name="role">Данные для обновления роли.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    /// <returns>Количество обновленных строк</returns>
    Task<bool?> UpdateRoleByIdAsync(Role role, CancellationToken cancellationToken);
    
    /// <summary>
    /// Проверяет существует ли роль с названием <param name="name">.</param> 
    /// </summary>
    /// <param name="name">Название роли</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Наличие роли в БД.</returns>
    Task<bool> RoleNameExist(string name, CancellationToken cancellationToken);
}