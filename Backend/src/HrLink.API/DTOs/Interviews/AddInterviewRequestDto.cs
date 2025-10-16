namespace HrLink.API.DTOs.Interviews;

public record AddInterviewRequestDto(Guid VacancyId, Guid CandidateId, Guid EmployeeId, DateTime InterviewDateTime);