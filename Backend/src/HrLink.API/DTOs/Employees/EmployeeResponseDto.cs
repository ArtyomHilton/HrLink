using HrLink.API.DTOs.Candidates;
using HrLink.API.DTOs.Interviews;
using HrLink.Domain.Entities;

namespace HrLink.API.DTOs.Employees;

public record EmployeeResponseDto
{
    public required Guid Id { get; set; }
    public required string Position { get; set; }
    public required string WorkEmail { get; set; }
    public string? WorkPhoneNumber { get; set; }
    public required DateTime DateOfEmployment { get; set; }
    public List<InterviewResponseDto>? Interview { get; set; }
}