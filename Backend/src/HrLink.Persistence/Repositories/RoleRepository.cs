using HrLink.Domain.Entities;
using HrLink.Domain.Interfaces;
using HrLink.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HrLink.Persistence.Repositories;

/// <summary>
/// <inheritdoc cref="IRoleRepository"/>
/// </summary>
public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _context;

    public RoleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<Role> AddRoleAsync(Role role, CancellationToken cancellationToken)
    {
        var entityEntry = await _context.AddAsync(role, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entityEntry.Entity;
    }

    /// <inheritdoc />
    public async Task<List<Role>> GetRolesAsync(CancellationToken cancellationToken) =>
        await _context.Roles.ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<Role?> GetRoleByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _context.Roles.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    /// <inheritdoc />
    public async Task<int?> UpdateRoleByIdAsync(Role role, CancellationToken cancellationToken) =>
        await _context.Roles
            .Where(x=> x.Id == role.Id)
            .ExecuteUpdateAsync(x =>
                x.SetProperty(r => r.Name, role.Name), cancellationToken);

    /// <inheritdoc />
    public async Task<int?> DeleteRoleById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _context.Database.BeginTransactionAsync(cancellationToken);

            await _context.UserRoles.Where(x => x.RoleId == id)
                .ExecuteDeleteAsync(cancellationToken);

            return await _context.Roles.Where(x => x.Id == id)
                .ExecuteDeleteAsync(cancellationToken);
        }
        catch (Exception exception)
        {
            return 0;
        }
    }
}