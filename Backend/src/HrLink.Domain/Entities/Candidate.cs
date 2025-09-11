namespace HrLink.Domain.Entities;

/// <summary>
/// Кандидат.
/// </summary>
public class Candidate
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Фамилия.
    /// </summary>
    public string SecondName { get; set; } = string.Empty;

    /// <summary>
    /// Отчество.
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Электронная почта.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Номер телефона.
    /// </summary>
    public string? PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Навигационное свойство с <see cref="Interview"/>.
    /// </summary>
    public ICollection<Interview>? Interviews { get; } = new List<Interview>();
}