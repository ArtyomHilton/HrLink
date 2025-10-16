namespace HrLink.API.DTOs.Candidates;

public record CandidateShortResponse(Guid Id, string FirstName, string SecondName, string? Patronymic);