using HrLink.Domain.Entities;
using HrLink.Domain.Interfaces.UserRepositories;
using HrLink.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HrLink.Persistence.Repositories.UserRepositories;

public class AddRolesForUserRepository : IAddRolesForUserRepository
{
    private readonly ApplicationDbContext _context;

    public AddRolesForUserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Guid>?> GetMissedRolesByUserIdFromListRoleIds(Guid userId, List<Guid>? roleIds,
        CancellationToken cancellationToken)
    {
        var existingUserRoleIds = await _context.UserRoles
            .Where(x => x.UserId == userId && roleIds.Contains(x.RoleId))
            .Select(x => x.RoleId)
            .ToListAsync(cancellationToken);

        return roleIds.Except(existingUserRoleIds).ToList();
    }

    public async Task<List<Guid>?> GetExistRolesByIdsAsync(List<Guid>? roleIds, CancellationToken cancellationToken) =>
        await _context.Roles
            .Where(x => roleIds!.Contains(x.Id))
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);

    public async Task<bool> AddRolesForUserByUserId(List<UserRole> userRoles, CancellationToken cancellationToken)
    {
        try
        {
            await _context.UserRoles.AddRangeAsync(userRoles, cancellationToken);

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
        catch (Exception exception)
        {
            return false;
        }
    }
}