namespace HrLink.Domain.Entities;

/// <summary>
/// Пользователь.
/// </summary>
public class User
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string SecondName { get; set; }
    public string? Patronymic { get; set; } = string.Empty;
    public required DateTime DateOfBirthday { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required bool IsDelete { get; set; }
    public Guid? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    
    public string FullName =>
        string.IsNullOrWhiteSpace(Patronymic)
            ? $"{SecondName} {FirstName}"
            : $"{SecondName} {FirstName} {Patronymic}";
}
