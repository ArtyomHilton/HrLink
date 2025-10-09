using HrLink.Domain.Entities;

namespace HrLink.Application.Common.Results.Errors;

public class UserHavingRoles : IError
{
    public string ErrorCode { get; init; }
    public string Target { get; init; }
    public Dictionary<string, object?>? Metadata { get; init; }

    public UserHavingRoles(string target)
    {
        ErrorCode = $"{nameof(User)}HavingRoles";
        Target = target;
    }
}