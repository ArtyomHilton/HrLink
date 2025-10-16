namespace HrLink.API.DTOs.Users;

public record AddUserRequest(string FirstName, string SecondName, string? Patronymic, DateTime DateOfBirthday, string Email, string Password, ICollection<Guid> RoleIds);