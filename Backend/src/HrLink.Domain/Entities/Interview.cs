namespace HrLink.Domain.Entities;

/// <summary>
/// Собеседование.
/// </summary>
public class Interview
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Идентификатор вакансии.
    /// Внешний ключ к <see cref="Vacancy"/>.
    /// </summary>
    public Guid VacancyId { get; set; }
    
    /// <summary>
    /// Навигационное свойство с <see cref="Vacancy"/>.
    /// </summary>
    public Vacancy Vacancy { get; set; } = null!;
    
    /// <summary>
    /// Идентификатор кандидата.
    /// Внешний ключ к <see cref="Candidate"/>.
    /// </summary>
    public Guid CandidateId { get; set; }
    
    /// <summary>
    /// Навигационное свойство с <see cref="Candidate"/>.
    /// </summary>
    public Candidate Candidate { get; set; } = null!;
    
    /// <summary>
    /// Идентификатор сотрудника.
    /// Внешний ключ к <see cref="Employee"/>.
    /// </summary>
    public Guid EmployeeId { get; set; }

    /// <summary>
    /// Навигационное свойство с <see cref="Employee"/>.
    /// </summary>
    public Employee Employee { get; set; } = null!;
    
    /// <summary>
    /// Дата и время начала собеседования.
    /// </summary>
    public DateTime InterviewDateTime { get; set; }
    
    public byte StatusId { get; set; }
    
    public Status Status { get; set; } = null!;
}