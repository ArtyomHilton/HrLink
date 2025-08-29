using HrLink.API.DTOs.Users;
using HrLink.Domain.Entities;

namespace HrLink.API.Mappings;

public static class UserMapping
{
    public static UserResponseDto ToDetailedResponse(this User user)
    {
        return new UserResponseDto()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            SecondName = user.SecondName,
            Patronymic = user.Patronymic,
            DateOfBirthday = user.DateOfBirthday,
            Email = user.Email,
            Employee = user.Employee?.ToDetailedResponse()
        };
    }

    public static List<UserResponseDto>? ToDetailedResponse(this List<User> users) =>
        users
            .Select(x => x.ToDetailedResponse())
            .ToList();

public static GetUsersResponseDto ToShortResponse(this User user)
{
    return new GetUsersResponseDto()
    {
        Id = user.Id,
        FirstName = user.FirstName,
        SecondName = user.SecondName,
        Patronymic = user.Patronymic,
        Employee = user.Employee?.ToShortResponse()
    };
}

public static List<GetUsersResponseDto> ToShortResponse(this List<User> users) =>
    users
        .Select(x => x.ToShortResponse())
        .ToList();

}