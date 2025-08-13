using HrLink.Domain.Entities;
using HrLink.Domain.Interfaces;
using HrLink.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HrLink.Persistence.Repositories;

/// <summary>
/// <inheritdoc cref="IUserRepository"/>
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<User> AddUserAsync(User user, CancellationToken cancellationToken)
    {
        var entryEntity = await _context.Users.AddAsync(user, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return entryEntity.Entity;
    }

    /// <inheritdoc />
    public async Task<List<User>?> GetUsersAsync(CancellationToken cancellationToken) =>
        await _context.Users
            .Include(x => x.Employee)
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .Where(x => !x.IsDelete)
            .ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _context.Users
            .Include(x => x.Employee)
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(x => !x.IsDelete && x.Id == id, cancellationToken);

    /// <inheritdoc />
    public async Task<int?> UpdateUserByIdAsync(User user, CancellationToken cancellationToken) =>
        await _context.Users
            .Where(x => x.Id == user.Id && !x.IsDelete)
            .ExecuteUpdateAsync(x => x
                .SetProperty(u => u.Email, user.Email)
                .SetProperty(u => u.DateOfBirthday, user.DateOfBirthday)
                .SetProperty(u => u.FirstName, user.FirstName)
                .SetProperty(u => u.SecondName, user.SecondName)
                .SetProperty(u => u.Patronymic, user.Patronymic), cancellationToken);

    /// <inheritdoc />
    public async Task<int?> DeleteOrRestoreUserByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _context.Users.Where(x => x.Id == id && !x.IsDelete)
            .ExecuteUpdateAsync(x => 
                x.SetProperty(u => u.IsDelete, u => !u.IsDelete), cancellationToken);
}