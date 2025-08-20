using HrLink.Application.UseCases.UserUseCases.AddRolesForUser;

namespace HrLink.API.DTOs.Users;

public class AddRolesForUserDto
{
    public List<Guid>? RoleIds { get; set; }

    public AddRolesForUserCommand ToCommand(Guid userId)
    {
        return new AddRolesForUserCommand()
        {
            UserId = userId,
            RoleIds = this.RoleIds
        };
    }
}