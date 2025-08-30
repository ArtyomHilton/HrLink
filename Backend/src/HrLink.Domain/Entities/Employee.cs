namespace HrLink.Domain.Entities;

/// <summary>
/// Сотрудник.
/// </summary>
public class Employee
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public required Guid Id { get; set; }
    
    /// <summary>
    /// Должность.
    /// </summary>
    public required string Position { get; set; }
    
    /// <summary>
    /// Рабочая электронная почта.
    /// </summary>
    public required string WorkEmail { get; set; }
    
    /// <summary>
    /// Рабочий номер телефона.
    /// </summary>
    public string? WorkPhoneNumber { get; set; }
    
    /// <summary>
    /// Дата начала работы.
    /// </summary>
    public required DateTime DateOfEmployment { get; set; }
    
    /// <summary>
    /// Дата последнего обновления.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
    
    /// <summary>
    /// Навигационное свойство с <see cref="User"/>.
    /// </summary>
    public User? User { get; set; }
    
    /// <summary>
    /// Навигационное свойство с <see cref="Interview"/>.
    /// </summary>
    public ICollection<Interview>? Interviews { get; } = new List<Interview>();
}