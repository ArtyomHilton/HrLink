using HrLink.Domain.Entities;

namespace HrLink.Domain.Interfaces.RoleRepositories;

public interface IGetRoleByIdRepository
{
    /// <summary>
    /// Получение роли по <paramref name="id"/>.
    /// </summary>
    /// <param name="id">Идентификатор <see cref="Role"/>.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    /// <returns>Полученную <see cref="Role"/> или <see cref="Nullable"/></returns>
    Task<Role?> GetRoleByIdAsync(Guid id, CancellationToken cancellationToken);
}