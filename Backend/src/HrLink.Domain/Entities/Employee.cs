namespace HrLink.Domain.Entities;

/// <summary>
/// Сотрудник.
/// </summary>
public class Employee
{
    public required Guid Id { get; set; }
    public required string Position { get; set; }
    public required string WorkEmail { get; set; }
    public string? WorkPhoneNumber { get; set; }
    public required DateTime DateOfEmployment { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public User? User { get; set; }
    public ICollection<Interview>? Interviews { get; } = new List<Interview>();
}