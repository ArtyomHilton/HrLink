using HrLink.Domain.Entities;
using HrLink.Domain.Interfaces.RoleRepositories;
using HrLink.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HrLink.Persistence.Repositories.RoleRepositories;

/// <inheritdoc cref="ICreateRoleRepository"/>
public class CreateRoleRepository : ICreateRoleRepository
{
    private readonly ApplicationDbContext _context;

    public CreateRoleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<Role> AddRoleAsync(Role role, CancellationToken cancellationToken)
    {
        var entry = await _context.Roles.AddAsync(role, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return entry.Entity;
    }
    
    /// <inheritdoc/>
    public async Task<bool> RoleNameExist(string name, CancellationToken cancellationToken) =>
        await _context.Roles.AnyAsync(x => x.Name.ToLower() == name.ToLower(), cancellationToken);
}