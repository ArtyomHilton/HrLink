namespace HrLink.API.DTOs.Users;

public record ChangeUserPasswordRequest(string Password, string NewPassword);