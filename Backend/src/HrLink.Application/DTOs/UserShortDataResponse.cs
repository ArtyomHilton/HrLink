namespace HrLink.Application.DTOs;

public record UserShortDataResponse(Guid Id, string FirstName, string SecondName, string? Patronymic, bool IsEmployee);