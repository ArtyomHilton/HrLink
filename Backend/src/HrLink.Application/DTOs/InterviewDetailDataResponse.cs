namespace HrLink.Application.DTOs;

public record InterviewDetailDataResponse(Guid Id, VacancyShortDataResponse Vacancy, CandidateShortDateResponse Candidate, EmployeeShortDataResponse Employee, DateTime InterviewDateTime, string Status);