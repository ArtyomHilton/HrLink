namespace HrLink.Application.DTOs;

public record CandidateShortDateResponse(Guid Id, string FirstName, string SecondName, string? Patronymic, string Email);