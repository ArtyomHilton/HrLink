using HrLink.Domain.Entities;

namespace HrLink.Domain.Interfaces.RoleRepositories;

public interface IDeleteRoleRepository
{
    /// <summary>
    /// Удаление <see cref="Role"/> по <paramref name="id"/>.
    /// </summary>
    /// <param name="id">Идентификатор роли.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
    /// <returns>Количество удаленных записей.</returns>
    Task<bool> DeleteRoleById(Guid id, CancellationToken cancellationToken);
}