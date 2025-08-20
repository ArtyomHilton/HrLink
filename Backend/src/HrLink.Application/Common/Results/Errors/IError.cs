namespace HrLink.Application.Common.Results.Errors;

public interface IError
{
    string Messsage { get; init; }
    string Target { get; init; }
}