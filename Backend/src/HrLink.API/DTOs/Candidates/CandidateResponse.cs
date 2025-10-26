namespace HrLink.API.DTOs.Candidates;

public record CandidateResponse
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string SecondName { get; set; }
    public string? Patronymic { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
}