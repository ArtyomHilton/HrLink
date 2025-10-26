namespace HrLink.API.DTOs.Auth;

public record LoginResponse(Guid Id,
    string FirstName,
    string SecondName,
    string? Patronymic,
    DateTime DateOfBirthday,
    string Email,
    List<RoleResponse> Roles);