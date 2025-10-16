using FluentValidation;
using HrLink.Application.UseCases.InterviewUseCases.AddInterview;

namespace HrLink.Application.Validators;

public class AddInterviewCommandValidator : AbstractValidator<AddInterviewCommand>
{
    public AddInterviewCommandValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(x => x.CandidateId)
            .NotEmpty()
            .WithErrorCode("CandidateEmpty");

        RuleFor(x => x.EmployeeId)
            .NotEmpty()
            .WithErrorCode("EmployeeEmpty");

        RuleFor(x => x.VacancyId)
            .NotEmpty()
            .WithErrorCode("VacancyEmpty");

        RuleFor(x => x.InterviewDateTime)
            .NotEmpty()
            .WithErrorCode("DateTimeInterviewEmpty")
            .GreaterThanOrEqualTo(DateTime.UtcNow.AddMinutes(30))
            .WithErrorCode("NotCorrectInterviewDateTime");
    }
}