namespace HrLink.Application.DTOs;

public record LoginDataResponse(
    Guid Id,
    string FirstName,
    string SecondName,
    string? Patronymic,
    DateTime DateOfBirthday,
    string Email,
    List<RoleDataResponse> Roles);