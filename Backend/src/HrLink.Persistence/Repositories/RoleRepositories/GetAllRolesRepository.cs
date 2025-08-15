using HrLink.Domain.Entities;
using HrLink.Domain.Interfaces.RoleRepositories;
using HrLink.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HrLink.Persistence.Repositories.RoleRepositories;

/// <inheritdoc cref="IGetAllRolesRepository"/>
public class GetAllRolesRepository : IGetAllRolesRepository
{
    private readonly ApplicationDbContext _context;

    public GetAllRolesRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<List<Role>> GetRolesAsync(CancellationToken cancellationToken) =>
        await _context.Roles.ToListAsync(cancellationToken);
}