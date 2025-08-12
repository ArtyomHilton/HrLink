namespace HrLink.Domain.Entities;

/// <summary>
/// Сотрудник.
/// </summary>
public class Employee
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string SecondName { get; set; }
    public string? Patronymic { get; set; } = string.Empty;
    public required string Position { get; set; }
    public required DateTime DateOfEmployment { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public User? User { get; set; }
}