namespace HrLink.Application.UseCases.UserUseCases.ChangePassword;

public record ChangeUserPasswordCommand(Guid Id, string Password, string NewPassword);