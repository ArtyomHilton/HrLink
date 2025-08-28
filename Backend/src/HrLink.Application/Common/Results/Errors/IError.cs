namespace HrLink.Application.Common.Results.Errors;

public interface IError
{
    string Message { get; init; }
    string Target { get; init; }
}