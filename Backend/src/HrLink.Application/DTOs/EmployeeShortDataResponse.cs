namespace HrLink.Application.DTOs;

public record EmployeeShortDataResponse(Guid Id, string FirstName, string SecondName, string? Patronymic);