namespace HrLink.Application.Common.Results.Errors;

public class Error : IError
{
    public string ErrorCode { get; init; }
    public string Target { get; init; }
    public Dictionary<string, object?> Metadata { get; init; }

    public Error(string target, Dictionary<string, object?>? metadata = null)
    {
        ErrorCode = "Произошла непредвиденная ошибка";
        Target = target;
        Metadata = metadata ?? [];
    }
}