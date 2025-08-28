namespace HrLink.Domain.Entities;

/// <summary>
/// Кандидат.
/// </summary>
public class Candidate
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string SecondName { get; set; }
    public string? Patronymic { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; } = string.Empty;
    public ICollection<Interview>? Interviews { get; } = new List<Interview>();
    public string FullName =>
        string.IsNullOrWhiteSpace(Patronymic)
            ? $"{SecondName} {FirstName}"
            : $"{SecondName} {FirstName} {Patronymic}";
}