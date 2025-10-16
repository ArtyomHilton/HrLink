using FluentValidation;
using HrLink.Application.UseCases.UserUseCases.GetUsers;

namespace HrLink.Application.Validators;

public class GetUsersQueryValidator : AbstractValidator<GetUsersQuery>
{
    public GetUsersQueryValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.ItemPerPage)
            .NotEmpty()
            .GreaterThanOrEqualTo(1)
            .WithErrorCode("NotCorrectPage");

        RuleFor(x => x.ItemPerPage)
            .NotEmpty()
            .GreaterThanOrEqualTo(1)
            .WithErrorCode("NotCorrectItemPerPage");
    }
}