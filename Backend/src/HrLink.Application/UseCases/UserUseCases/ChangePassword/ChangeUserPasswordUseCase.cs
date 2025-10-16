using FluentValidation;
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
    private readonly IValidator<ChangeUserPasswordCommand> _validator;

    public ChangeUserPasswordUseCase(IApplicationDbContext context, IValidator<ChangeUserPasswordCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<Result> Execute(ChangeUserPasswordCommand command, CancellationToken cancellationToken)
    {
        var validateResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validateResult.IsValid)
        {
            return Result.Failure(new ValidateError(validateResult.Errors[0].ErrorCode, validateResult.Errors[0].PropertyName));
        }
        
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