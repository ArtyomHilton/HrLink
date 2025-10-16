namespace HrLink.Application.DTOs;

public record EmployeeDetailDataResponse(Guid Id, string Position, string WorkEmail, string? WorkPhoneNumber, DateTime DateOfEmployment, List<InterviewShortDataResponse> Interviews);