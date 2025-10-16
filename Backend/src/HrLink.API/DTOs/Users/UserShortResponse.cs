namespace HrLink.API.DTOs.Users;

public record UserShortResponse(Guid Id, string FirstName, string SecondName, string? Patronymic, bool IsEmployee);
