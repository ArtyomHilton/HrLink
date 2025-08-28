namespace HrLink.Application.Common.Results.Errors;

public class UserEmailExistError : IError
{
    public string Message { get; init; }
    public string Target { get; init; }

    public UserEmailExistError(string message, string target)
    {
        Message = message;
        Target = target;
    }
}