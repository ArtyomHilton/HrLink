using HrLink.API.DTOs.Interviews;
using HrLink.Application.Common.Results;
using HrLink.Application.Common.Results.Errors;

namespace HrLink.API.Validators;

public static class InterviewValidators
{
    private static readonly string ErrorValidateNotEmptyMessage = "{0} cannot be empty";
    private static readonly string ErrorValidateInterviewDateTimeMustBeFutureDate = "{0} date must be in the future";

    private static readonly int MinTimeForPreparation = 10;

    public static Result Validate(this AddInterviewRequestDto dto)
    {
        if (dto.VacancyId == Guid.Empty || dto.VacancyId is null)
        {
            return Result.Failure(new ValidateError(string.Format(ErrorValidateNotEmptyMessage, nameof(dto.VacancyId)), nameof(dto.VacancyId)));
        }
        
        if (dto.CandidateId == Guid.Empty || dto.CandidateId is null)
        {
            return Result.Failure(new ValidateError(string.Format(ErrorValidateNotEmptyMessage, nameof(dto.CandidateId)), nameof(dto.CandidateId)));
        }
        
        if (dto.EmployeeId == Guid.Empty || dto.EmployeeId is null)
        {
            return Result.Failure(new ValidateError(string.Format(ErrorValidateNotEmptyMessage, nameof(dto.EmployeeId)), nameof(dto.EmployeeId)));
        }

        if (dto.InterviewDateTime is null)
        {
            return Result.Failure(new ValidateError(string.Format(ErrorValidateNotEmptyMessage, nameof(dto.InterviewDateTime)), nameof(dto.InterviewDateTime)));
        }

        if (dto.InterviewDateTime.Value < DateTime.UtcNow.AddMinutes(MinTimeForPreparation))
        {
            return Result.Failure(new ValidateError(string.Format(ErrorValidateInterviewDateTimeMustBeFutureDate, nameof(dto.InterviewDateTime)), nameof(dto.InterviewDateTime)));
        }

        return Result.Success();
    }
}