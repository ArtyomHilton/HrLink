using FluentValidation;
using HrLink.Application.UseCases.UserUseCases.ChangePassword;

namespace HrLink.Application.Validators;

public class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
{
    public ChangeUserPasswordCommandValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithErrorCode("UserEmpty");
        
        RuleFor(x=> x.Password)
            .NotEmpty()
            .WithErrorCode("PasswordEmpty")
            .MinimumLength(6)
            .WithErrorCode("NotCorrectPasswordLength")
            .Matches(@"^[A-Za-z0-9!@#$%&?*()]{6,}$")
            .WithErrorCode("NotCorrectPasswordFormat");
        
        RuleFor(x=> x.NewPassword)
            .NotEmpty()
            .WithErrorCode("PasswordEmpty")
            .NotEqual(x=> x.Password)
            .WithErrorCode("NewPasswordEqualPassword")
            .MinimumLength(6)
            .WithErrorCode("NotCorrectPasswordLength")
            .Matches(@"^[A-Za-z0-9!@#$%&?*()]{6,}$")
            .WithErrorCode("NotCorrectPasswordFormat");
    }
}