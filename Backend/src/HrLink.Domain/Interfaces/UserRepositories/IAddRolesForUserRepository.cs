using HrLink.Domain.Entities;

namespace HrLink.Domain.Interfaces.UserRepositories;

public interface IAddRolesForUserRepository
{
    Task<List<Guid>?> GetMissedRolesByUserIdFromListRoleIds(Guid userId, List<Guid>? roleIds,
        CancellationToken cancellationToken);

    Task<List<Guid>?> GetExistRolesByIdsAsync(List<Guid>? roleIds, CancellationToken cancellationToken);
    Task<bool> AddRolesForUserByUserId(List<UserRole> userRoles, CancellationToken cancellationToken);
}