namespace HrLink.API.DTOs.Interviews;

public record AddInterviewRequest(Guid VacancyId, Guid CandidateId, Guid EmployeeId, DateTime InterviewDateTime);