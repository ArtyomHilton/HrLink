using HrLink.Domain.Entities;

namespace HrLink.Application.UseCases.UserUseCases.AddRolesForUser;

public class AddRolesForUserCommand
{
    public Guid UserId { get; set; }
    public List<Guid>? RoleIds { get; set; }

    public List<UserRole> ToModel()
    {
        return RoleIds!.Select(x => new UserRole()
        {
            UserId = this.UserId,
            RoleId = x
        }).ToList();
    }
}