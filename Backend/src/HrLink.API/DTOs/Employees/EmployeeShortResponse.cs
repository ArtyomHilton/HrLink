namespace HrLink.API.DTOs.Employees;

public record EmployeeShortResponse(Guid Id, string FirstName, string SecondName, string? Patronymic);