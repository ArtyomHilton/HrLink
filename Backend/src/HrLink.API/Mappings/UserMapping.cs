using HrLink.API.DTOs.Users;
using HrLink.Domain.Entities;

namespace HrLink.API.Mappings;

public static class UserMapping
{
    public static UserResponseDto ToResponse(this User user)
    {
        return new UserResponseDto()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            SecondName = user.SecondName,
            Patronymic = user.Patronymic,
            DateOfBirthday = user.DateOfBirthday,
            Email = user.Email,
            Employee = user.Employee?.ToResponse()
        };
    }
}