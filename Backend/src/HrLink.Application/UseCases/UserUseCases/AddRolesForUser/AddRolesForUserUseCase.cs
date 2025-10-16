using FluentValidation;
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

    private readonly IValidator<AddRolesForUserCommand> _validator;

    public AddRolesForUserUseCase(IApplicationDbContext context, IValidator<AddRolesForUserCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    /// <inheritdoc />
    public async Task<Result> Execute(AddRolesForUserCommand command, CancellationToken cancellationToken)
    {
        var validateResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validateResult.IsValid)
        {
            return Result.Failure(new ValidateError(validateResult.Errors[0].ErrorCode,validateResult.Errors[0].PropertyName));
        }
        
        command.RoleIds = await _context.Roles
            .Where(x => command.RoleIds!.Contains(x.Id))
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);

        if (!command.RoleIds!.Any())
            return Result.Failure(new ValidateError("RolesNotExist", nameof(command.RoleIds)));

        var userRolesIds = await _context.UserRoles
            .Where(x => x.UserId == command.UserId && command.RoleIds.Contains(x.RoleId))
            .Select(x => x.RoleId)
            .ToListAsync(cancellationToken);

        command.RoleIds = command.RoleIds
            .Except(userRolesIds)
            .ToList();

        if (!command.RoleIds!.Any())
            return Result.Failure(new UserHavingRoles(nameof(command.RoleIds)));

        await _context.UserRoles.AddRangeAsync(command.ToEntity(), cancellationToken);
        
        return await _context.SaveChangesAsync(cancellationToken) > 0
            ? Result.Success()
            : Result.Failure(new Error(nameof(command.RoleIds)));
    }
}