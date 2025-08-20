namespace HrLink.Application.Common.Results.Errors;

public class NoRolesError : IError
{
    public string Messsage { get; init; }
    public string Target { get; init; }

    public NoRolesError(string messsage, string target)
    {
        Messsage = messsage;
        Target = target;
    }
}