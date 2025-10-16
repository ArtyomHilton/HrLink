namespace HrLink.Application.DTOs;

public record InterviewShortDataResponse(Guid Id, string VacancyName, DateTime InterviewDateTime, string Status);