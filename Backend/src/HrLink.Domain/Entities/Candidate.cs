namespace HrLink.Domain.Entities;

/// <summary>
/// Кандидат.
/// </summary>
public class Candidate
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public required Guid Id { get; set; }
    
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
    public string? Patronymic { get; set; }
    
    /// <summary>
    /// Электронная почта.
    /// </summary>
    public required string Email { get; set; }
    
    /// <summary>
    /// Номер телефона.
    /// </summary>
    public string? PhoneNumber { get; set; } = string.Empty;
    
    /// <summary>
    /// Навигационное свойство с <see cref="Interview"/>.
    /// </summary>
    public ICollection<Interview>? Interviews { get; } = new List<Interview>();
}