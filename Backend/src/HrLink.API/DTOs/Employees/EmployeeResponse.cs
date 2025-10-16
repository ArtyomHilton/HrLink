using HrLink.API.DTOs.Candidates;
using HrLink.API.DTOs.Interviews;
using HrLink.Domain.Entities;

namespace HrLink.API.DTOs.Employees;

public record EmployeeResponse(
    Guid Id,
    string Position,
    string WorkEmail,
    string? WorkPhoneNumber,
    string DateOfEmployment,
    List<InterviewShortResponse>? Interview
);