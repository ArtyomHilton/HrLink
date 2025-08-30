using HrLink.Domain.Entities;

namespace HrLink.Application.UseCases.UserUseCases.AddRolesForUser;

/// <summary>
/// Команда добавления ролей для пользователя.
/// </summary>
public class AddRolesForUserCommand
{
    /// Идентификатор пользователя.
    public Guid UserId { get; set; }
    
    /// Список идентификаторов ролей.
    public List<Guid>? RoleIds { get; set; }

    /// Маппит команду в доменную сущность <see cref="UserRole"/>. 
    public List<UserRole> ToEntity()
    {
        return RoleIds!.Select(x => new UserRole()
        {
            UserId = this.UserId,
            RoleId = x
        }).ToList();
    }
}