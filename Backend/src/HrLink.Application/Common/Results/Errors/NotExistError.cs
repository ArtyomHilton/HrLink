namespace HrLink.Application.Common.Results.Errors;

public class NotExistError<T> : IError
{
    public string Message { get; init; }
    public string Target { get; init; }

    public NotExistError(Guid entityId)
    {
        Message = $"{typeof(T).Name} with Id: {entityId} no exist";
        Target = typeof(T).Name;
    }
}