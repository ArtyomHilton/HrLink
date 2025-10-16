namespace HrLink.Application.Common.Results.Errors;

public interface INotFoundError : IError { }

public class NotFoundError<T> : INotFoundError
{
    public string ErrorCode { get; init; }
    public string Target { get; init; }
    public Dictionary<string, object?> Metadata { get; init; }

    public NotFoundError(string target, Dictionary<string, object?>? metadata = null)
    {
        ErrorCode = $"{typeof(T).Name}NotFound";
        Target = target;
        Metadata = metadata ?? [];
    }
}