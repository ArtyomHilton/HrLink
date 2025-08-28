namespace HrLink.Application.Common.Results.Errors;

public class Error : IError
{
    public string Message { get; init; }
    public string Target { get; init; }

    public Error(string target)
    {
        Message = "Произошла непредвиденная ошибка";
        Target = target;
    }
}