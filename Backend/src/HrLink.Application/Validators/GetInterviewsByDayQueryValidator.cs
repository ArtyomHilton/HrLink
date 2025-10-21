using FluentValidation;
using HrLink.Application.UseCases.InterviewUseCases.GetInterviewByDay;

namespace HrLink.Application.Validators;

public class GetInterviewsByDayQueryValidator : AbstractValidator<GetInterviewsByDateQuery>
{
    public GetInterviewsByDayQueryValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.InterviewDate)
            .NotEmpty()
            .WithErrorCode("DateEmpty")
            .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
            .WithErrorCode("DateNotBeLessNowDate");
    }
}