using FluentValidation;
using HrLink.Application.UseCases.UserUseCases.AddRolesForUser;

namespace HrLink.Application.Validators;

public class AddRolesForUserCommandValidator : AbstractValidator<AddRolesForUserCommand>
{
    public AddRolesForUserCommandValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithErrorCode("UserEmpty");

        RuleFor(x => x.RoleIds)
            .NotEmpty()
            .WithErrorCode("NoRoles");
    }
}