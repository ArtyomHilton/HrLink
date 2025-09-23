namespace HrLink.API.DTOs.Users;

public record ChangeUserPasswordRequestDto(string Password, string NewPassword);