using HrLink.Application.Common.Results;
using HrLink.Application.Common.Results.Errors;
using HrLink.Application.Interfaces;
using HrLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HrLink.Application.UseCases.UserUseCases.AddUser;

/// <see cref="IAddUserUseCase"/>
public class AddUserUseCase : IAddUserUseCase
{
    /// <inheritdoc cref="IApplicationDbContext"/>
    private readonly IApplicationDbContext _context;

    public AddUserUseCase(IApplicationDbContext context)
    {
        _context = context;
    }
    
    /// <inheritdoc />
    public async Task<Result<User?>> Execute(AddUserCommand command, CancellationToken cancellationToken)
    {
        if (await _context.Users.AnyAsync(x=> x.Email == command.Email, cancellationToken))
        {
            return Result.Failure<User?>(null, new UserEmailExistError("Данные Email уже занят", nameof(command.Email)));
        }

        command.RoleIds = await _context.Roles
            .Where(x=> command.RoleIds.Contains(x.Id))
            .Select(x=> x.Id)
            .ToListAsync(cancellationToken);

        var entry = await _context.Users
            .AddAsync(command.ToEntity(), cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success<User?>(entry.Entity);
    }
}