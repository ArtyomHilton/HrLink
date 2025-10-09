namespace HrLink.Application.Common.Results.Errors;

public class NotFoundError<T> : IError
{
    public string ErrorCode { get; init; }
    public string Target { get; init; }
    public Dictionary<string, object?>? Metadata { get; init; }

    public NotFoundError(string target, Dictionary<string, object?>? metadata = null)
    {
        ErrorCode = $"{typeof(T).Name}NotFound";
        Target = target;
        Metadata = metadata;
    }
}