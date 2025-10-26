namespace HrLink.Application.Common.Results.Errors;

public class UnAuthorizedError : IError
{
    public string ErrorCode { get; init; }
    public string Target { get; init; }
    public Dictionary<string, object?> Metadata { get; init; }

    public UnAuthorizedError(Dictionary<string, object?>? metadata = null)
    {
        ErrorCode = "Unauthorized";
        Target = "Auth data";
        Metadata = metadata ?? [];
    }
}