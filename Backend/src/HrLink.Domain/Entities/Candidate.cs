namespace HrLink.Domain.Entities;

/// <summary>
/// Кандидат.
/// </summary>
public class Candidate
{
    public required Guid Id { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; } = string.Empty;
}