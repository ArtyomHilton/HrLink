using HrLink.Domain.Entities;
using HrLink.Domain.Interfaces.RoleRepositories;
using HrLink.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HrLink.Persistence.Repositories.RoleRepositories;

/// <inheritdoc cref="IUpdateRoleByIdRepository"/>
public class UpdateRoleByIdRepository : IUpdateRoleByIdRepository
{
    private readonly ApplicationDbContext _context;

    public UpdateRoleByIdRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<bool?> UpdateRoleByIdAsync(Role role, CancellationToken cancellationToken)
    {
        var updateResult = await _context.Roles
            .Where(x => x.Id == role.Id)
            .ExecuteUpdateAsync(x =>
                x.SetProperty(r => r.Name, role.Name), cancellationToken);

        if (updateResult > 0)
            return true;

        return await _context.Roles
            .AnyAsync(x => x.Id == role.Id, cancellationToken) 
            ? false 
            : null;
    }

    /// <inheritdoc/>
    public async Task<bool> RoleNameExist(string name, CancellationToken cancellationToken) =>
        await _context.Roles.AnyAsync(x => x.Name.ToLower() == name.ToLower(), cancellationToken);
}