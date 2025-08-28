namespace HrLink.Application.Common.Results.Errors;

public class NotFoundError : IError
{
    public string Message { get; init; }
    public string Target { get; init; }

    public NotFoundError(string message, string target)
    {
        Message = message;
        Target = target;
    }
}