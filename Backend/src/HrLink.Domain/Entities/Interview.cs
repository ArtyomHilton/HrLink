namespace HrLink.Domain.Entities;

/// <summary>
/// Собеседование.
/// </summary>
public class Interview
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public required Guid Id { get; set; }
    
    /// <summary>
    /// Идентификатор кандидата.
    /// Внешний ключ к <see cref="Candidate"/>.
    /// </summary>
    public required Guid CandidateId { get; set; }
    
    /// <summary>
    /// Навигационное свойство с <see cref="Candidate"/>.
    /// </summary>
    public Candidate? Candidate { get; set; }
    
    /// <summary>
    /// Идентификатор сотрудника.
    /// Внешний ключ к <see cref="Employee"/>.
    /// </summary>
    public required Guid EmployeeId { get; set; }
    
    /// <summary>
    /// Навигационное свойство с <see cref="Employee"/>.
    /// </summary>
    public Employee? Employee { get; set; }
    
    /// <summary>
    /// Дата и время начала собеседования.
    /// </summary>
    public required DateTime InterviewDateTime { get; set; }
}