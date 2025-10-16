using HrLink.API.DTOs.Employees;

namespace HrLink.API.DTOs.Users;

public record UserDetailResponse(
    Guid Id,
    string FirstName,
    string SecondName,
    string? Patronymic,
    string DateOfBirthday,
    string Email,
    EmployeeResponse? Employee);