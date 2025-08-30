namespace HrLink.Domain.Entities;

/// <summary>
/// Пользователь.
/// </summary>
public class User
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Имя.
    /// </summary>
    public required string FirstName { get; set; }
    
    /// <summary>
    /// Фамилия.
    /// </summary>
    public required string SecondName { get; set; }
    
    /// <summary>
    /// Отчество.
    /// </summary>
    public string? Patronymic { get; set; } = string.Empty;
    
    /// <summary>
    /// Дата рождения.
    /// </summary>
    public required DateTime DateOfBirthday { get; set; }
    
    /// <summary>
    /// Электронная почта.
    /// </summary>
    public required string Email { get; set; }
    
    /// <summary>
    /// Хэш пароля.
    /// </summary>
    public required string PasswordHash { get; set; }
    
    /// <summary>
    /// Статус удаления.
    /// </summary>
    public required bool IsDelete { get; set; }
    
    /// <summary>
    /// Идентификатор сотрудника.
    /// Внешний ключ к <see cref="Employee"/>.
    /// </summary>
    public Guid? EmployeeId { get; set; }
    
    /// <summary>
    /// Навигационное свойство к <see cref="Employee"/>.
    /// </summary>
    public Employee? Employee { get; set; }
    
    /// <summary>
    /// Навигационное свойство к <see cref="UserRole"/>.
    /// </summary>
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
