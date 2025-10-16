using HrLink.Domain.Entities;

namespace HrLink.Application.Common.Results.Errors;

public class UserHavingRoles : IValidateError
{
    public string ErrorCode { get; init; }
    public string Target { get; init; }
    public Dictionary<string, object?> Metadata { get; init; }

    public UserHavingRoles(string target, Dictionary<string, object?>? metadata = null)
    {
        ErrorCode = $"{nameof(User)}HavingRoles";
        Target = target;
        Metadata = metadata ?? [];
    }
}