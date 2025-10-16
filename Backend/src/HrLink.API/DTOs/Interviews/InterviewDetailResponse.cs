using HrLink.API.DTOs.Candidates;
using HrLink.API.DTOs.Employees;
using HrLink.API.DTOs.Vacancy;

namespace HrLink.API.DTOs.Interviews;

public record InterviewDetailResponse(Guid Id, VacancyShortResponse Vacancy, CandidateShortResponse Candidate, EmployeeShortResponse Employee, string InterviewDateTime, string Status);