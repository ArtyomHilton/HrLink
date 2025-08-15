using HrLink.Domain.Interfaces.RoleRepositories;
using HrLink.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HrLink.Persistence.Repositories.RoleRepositories;

/// <inheritdoc cref="IDeleteRoleRepository"/>
public class DeleteRoleRepository : IDeleteRoleRepository
{
    private readonly ApplicationDbContext _context;

    public DeleteRoleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteRoleById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _context.Database.BeginTransactionAsync(cancellationToken);

            await _context.UserRoles
                .Where(x => x.RoleId == id)
                .ExecuteDeleteAsync(cancellationToken);

            var deleteResult = await _context.Roles
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync(cancellationToken);

            await _context.Database.CommitTransactionAsync(cancellationToken);

            return deleteResult > 0;
        }
        catch (Exception exception)
        {
            await _context.Database.RollbackTransactionAsync(cancellationToken);

            return false;
        }
    }
}