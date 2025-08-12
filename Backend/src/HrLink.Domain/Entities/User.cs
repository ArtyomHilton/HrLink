namespace HrLink.Domain.Entities;

/// <summary>
/// Пользователь.
/// </summary>
public class User
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required bool IsDelete { get; set; }
    public Guid? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}
