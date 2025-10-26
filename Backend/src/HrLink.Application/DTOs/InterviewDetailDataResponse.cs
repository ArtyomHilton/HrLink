namespace HrLink.Application.DTOs;

public record InterviewDetailDataResponse(Guid Id, VacancyShortDataResponse Vacancy, CandidateShortDataResponse Candidate, EmployeeShortDataResponse Employee, DateTime InterviewDateTime, string Status);