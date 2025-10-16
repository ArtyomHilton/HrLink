using FluentValidation;
using HrLink.Application.UseCases.UserUseCases.GetUserByIdUser;

namespace HrLink.Application.Validators;

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithErrorCode("UserEmpty");
    }
}