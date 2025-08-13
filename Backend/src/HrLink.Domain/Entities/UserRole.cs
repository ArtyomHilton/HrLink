namespace HrLink.Domain.Entities;

public class UserRole
{
    public required Guid UserId { get; set; }
    public User? User { get; set; }
    public required Guid RoleId { get; set; }
    public Role? Role { get; set; }
}