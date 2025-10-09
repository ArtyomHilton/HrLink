using System.ComponentModel.DataAnnotations;
using FluentValidation;
using HrLink.Application.UseCases.InterviewUseCases.AddInterview;

namespace HrLink.Application.Validators;

public class AddInterviewCommandValidator : AbstractValidator<AddInterviewCommand>
{
    public AddInterviewCommandValidator()
    {
        RuleFor(x => x.CandidateId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("CandidateEmpty");

        RuleFor(x => x.EmployeeId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("EmployeeEmpty");

        RuleFor(x => x.VacancyId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("VacancyEmpty");

        RuleFor(x => x.InterviewDateTime)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("DateTimeInterviewEmpty")
            .GreaterThanOrEqualTo(DateTime.UtcNow.AddMinutes(30))
            .WithErrorCode("NotCorrectInterviewDateTime");
    }
}