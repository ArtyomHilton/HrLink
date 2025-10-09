namespace HrLink.Application.Common.Results.Errors;

public class ValidateError : IError
{
    public string ErrorCode { get; init; }
    public string Target { get; init; }
    public Dictionary<string, object?>? Metadata { get; init; }

    public ValidateError(string errorCode, string target, Dictionary<string, object?>? metadata = null)
    {
        ErrorCode = errorCode;
        Target = target;
        Metadata = metadata;
    }
}