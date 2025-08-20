namespace HrLink.Application.Common.Results.Errors;

public class Error : IError
{
    public string Messsage { get; init; }
    public string Target { get; init; }

    public Error(string target)
    {
        Messsage = "Произошла непредвиденная ошибка";
        Target = target;
    }
}