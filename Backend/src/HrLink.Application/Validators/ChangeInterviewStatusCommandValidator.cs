using FluentValidation;
using HrLink.Application.UseCases.InterviewUseCases.ChangeInterviewStatus;
using HrLink.Domain.Enums;

namespace HrLink.Application.Validators;

public class ChangeInterviewStatusCommandValidator : AbstractValidator<ChangeInterviewStatusCommand>
{
    public ChangeInterviewStatusCommandValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(x => x.InterviewId)
            .NotEmpty()
            .WithErrorCode("InterviewEmpty");

        RuleFor(x => x.StatusId)
            .NotEmpty()
            .WithErrorCode("StatusEmpty")
            .Must(BeValidateStatus)
            .WithErrorCode("NotCorrectStatus");
    }

    private bool BeValidateStatus(byte statusId) =>
        Enum.IsDefined(typeof(Status), statusId);
}