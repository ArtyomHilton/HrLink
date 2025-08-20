using HrLink.Domain.Entities;
using HrLink.Domain.Interfaces.UserRepositories;
using HrLink.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HrLink.Persistence.Repositories.UserRepositories;

public class AddUserRepository : IAddUserRepository
{
    private readonly ApplicationDbContext _context;

    public AddUserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Guid>> GetExistRolesByIdsAsync(List<Guid> roleIds, CancellationToken cancellationToken) =>
        await _context.Roles
            .Where(x => roleIds.Contains(x.Id))
            .Select(x=> x.Id)
            .ToListAsync(cancellationToken);

    public async Task<bool> EmailExistAsync(string email, CancellationToken cancellationToken) =>
        await _context.Users.AnyAsync(x => x.Email == email, cancellationToken);

    public async Task<User?> AddUserAsync(User user, CancellationToken cancellationToken)
    {
        var entry = await _context.Users.AddAsync(user, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
        
        return entry.Entity;
    }
}