namespace HrLink.Application.Common.Results.Errors;

public class UserEmailExistError : IError
{
    public string Messsage { get; init; }
    public string Target { get; init; }

    public UserEmailExistError(string message, string target)
    {
        Messsage = message;
        Target = target;
    }
}