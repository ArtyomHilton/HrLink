namespace HrLink.Domain.Entities;

/// <summary>
/// Роль.
/// </summary>
public class Role
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public required Guid Id { get; set; }
    
    /// <summary>
    /// Название.
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Навигационное свойство с <see cref="UserRole"/>.
    /// </summary>
    public ICollection<UserRole> UserRoles { get; set; }= new List<UserRole>();
}