namespace HrLink.Application.DTOs;

public record UserDetailDataResponse(Guid Id, string FirstName, string SecondName, string? Patronymic,DateTime DateOfBirthday, string Email, EmployeeDetailDataResponse? Employee);