namespace HrLink.Application.Common.Results.Errors;

public interface IError
{
    string ErrorCode { get; init; }
    string Target { get; init; }
    
    Dictionary<string, object?> Metadata { get; init; } 
}