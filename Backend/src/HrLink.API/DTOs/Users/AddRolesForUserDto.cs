using HrLink.Application.UseCases.UserUseCases.AddRolesForUser;

namespace HrLink.API.DTOs.Users;

public record AddRolesForUserDto
{
    public ICollection<Guid> RoleIds { get; set; } = new List<Guid>();

    public AddRolesForUserCommand ToCommand(Guid userId)
    {
        return new AddRolesForUserCommand()
        {
            UserId = userId,
            RoleIds = this.RoleIds
        };
    }
}