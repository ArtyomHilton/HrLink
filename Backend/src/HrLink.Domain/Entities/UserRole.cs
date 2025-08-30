namespace HrLink.Domain.Entities;

/// <summary>
/// Роли пользователя.
/// </summary>
public class UserRole
{
    /// <summary>
    /// Идентификатор пользователя.
    /// Внешний ключ к <see cref="User"/>. 
    /// </summary>
    public required Guid UserId { get; set; }
    
    /// <summary>
    /// Навигационное свойство с <see cref="User"/>.
    /// </summary>
    public User? User { get; set; }
    
    /// <summary>
    /// Идентификатор роли.
    /// Внешний ключ к <see cref="Role"/>.
    /// </summary>
    public required Guid RoleId { get; set; }
    
    /// <summary>
    /// Навигационное свойство с <see cref="Role"/>.
    /// </summary>
    public Role? Role { get; set; }
}