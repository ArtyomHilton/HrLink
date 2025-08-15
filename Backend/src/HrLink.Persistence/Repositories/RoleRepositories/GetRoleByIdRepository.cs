using HrLink.Domain.Entities;
using HrLink.Domain.Interfaces.RoleRepositories;
using HrLink.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HrLink.Persistence.Repositories.RoleRepositories;

/// <inheritdoc cref="IGetRoleByIdRepository"/>
public class GetRoleByIdRepository : IGetRoleByIdRepository
{
    private readonly ApplicationDbContext _context;

    public GetRoleByIdRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<Role?> GetRoleByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _context.Roles.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
}