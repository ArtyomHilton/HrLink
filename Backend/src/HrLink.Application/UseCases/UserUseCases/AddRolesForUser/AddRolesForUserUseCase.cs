using HrLink.Application.Common.Results;
using HrLink.Application.Common.Results.Errors;
using HrLink.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HrLink.Application.UseCases.UserUseCases.AddRolesForUser;

/// <inheritdoc cref="IAddRolesForUserUseCase"/>
public class AddRolesForUserUseCase : IAddRolesForUserUseCase
{
    /// <inheritdoc cref="IApplicationDbContext"/>
    private readonly IApplicationDbContext _context;

    public AddRolesForUserUseCase(IApplicationDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<Result> Execute(AddRolesForUserCommand command, CancellationToken cancellationToken)
    {
        command.RoleIds = await _context.Roles
            .Where(x => command.RoleIds!.Contains(x.Id))
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);

        if (!command.RoleIds!.Any())
            return Result.Failure(new NoRolesError("Указанные вами роли не существуют", nameof(command.RoleIds)));

        var userRolesIds = await _context.UserRoles
            .Where(x => x.UserId == command.UserId && command.RoleIds.Contains(x.RoleId))
            .Select(x => x.RoleId)
            .ToListAsync(cancellationToken);

        command.RoleIds = command.RoleIds
            .Except(userRolesIds)
            .ToList();

        if (!command.RoleIds!.Any())
            return Result.Failure(new NoRolesError("Указанные вами роли уже присутствуют у этого пользователя",
                nameof(command.RoleIds)));

        await _context.UserRoles.AddRangeAsync(command.ToEntity(), cancellationToken);
        
        return await _context.SaveChangesAsync(cancellationToken) > 0
            ? Result.Success()
            : Result.Failure(new Error(nameof(command.RoleIds)));
    }
}