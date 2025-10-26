namespace HrLink.Application.DTOs;

public record CandidateShortDataResponse(Guid Id, string FirstName, string SecondName, string? Patronymic, string Email);