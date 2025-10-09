using HrLink.Application.Common;
using HrLink.Application.Common.Results;
using HrLink.Application.Common.Results.Errors;
using HrLink.Application.Interfaces;
using HrLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HrLink.Application.UseCases.UserUseCases.ChangePassword;

public class ChangeUserPasswordUseCase : IChangeUserPasswordUseCase
{
    private readonly IApplicationDbContext _context;

    public ChangeUserPasswordUseCase(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Execute(ChangeUserPasswordCommand command, CancellationToken cancellationToken)
    {
        var currentHashPassword = await _context.Users
            .Where(x => x.Id == command.Id)
            .Select(x => x.PasswordHash)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (currentHashPassword is null)
        {
            return Result.Failure(new NotFoundError<User>(nameof(command.Id), 
                new Dictionary<string, object?>() { ["userId"] = command.Id}));
        }

        if (!PasswordHasher.VerifyPassword(command.Password, currentHashPassword))
        {
            return Result.Failure(new ValidateError("IncorrectPassword", nameof(command.Password)));
        }

        if (PasswordHasher.VerifyPassword(command.NewPassword, currentHashPassword))
        {
            return Result.Failure(new ValidateError("NewPasswordSameAsCurrent", nameof(command.NewPassword)));
        }

        await _context.Users.Where(x=> x.Id == command.Id)
            .ExecuteUpdateAsync(x => 
                x.SetProperty(u => u.PasswordHash, PasswordHasher.HashPassword(command.NewPassword)), cancellationToken);

        return Result.Success();
    }
}