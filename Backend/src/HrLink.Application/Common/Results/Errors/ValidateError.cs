namespace HrLink.Application.Common.Results.Errors;

public class ValidateError : IError
{
    public string Message { get; init; }
    public string Target { get; init; }

    public ValidateError(string message, string target)
    {
        Message = message;
        Target = target;
    }
}