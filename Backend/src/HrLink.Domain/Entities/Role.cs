namespace HrLink.Domain.Entities;

public class Role
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }= new List<UserRole>();
}