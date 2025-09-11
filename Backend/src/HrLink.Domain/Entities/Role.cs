namespace HrLink.Domain.Entities;

/// <summary>
/// Роль.
/// </summary>
public class Role
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Навигационное свойство с <see cref="UserRole"/>.
    /// </summary>
    public ICollection<UserRole> UserRoles { get; set; }= new List<UserRole>();
}