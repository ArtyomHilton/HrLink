using HrLink.API.DTOs.Candidates;

namespace HrLink.API.DTOs.Interviews;

public record InterviewShortResponse(Guid Id, string VacancyName, string InterviewDateTime, string Status);