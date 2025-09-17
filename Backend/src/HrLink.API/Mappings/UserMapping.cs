using HrLink.API.DTOs.Users;
using HrLink.Application.UseCases.UserUseCases.AddRolesForUser;
using HrLink.Application.UseCases.UserUseCases.AddUser;
using HrLink.Application.UseCases.UserUseCases.GetUsers;
using HrLink.Domain.Entities;

namespace HrLink.API.Mappings;

/// <summary>
/// Мапперы для <see cref="User"/>.
/// </summary>
public static class UserMapping
{
    /// <summary>
    /// Детальный маппинг <see cref="User"/> в <see cref="UserResponseDto"/> 
    /// </summary>
    /// <param name="user"></param>
    /// <returns><see cref="UserResponseDto"/></returns>
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

    /// <summary>
    /// Детальный маппинг списка <see cref="User"/> в список<see cref="UserResponseDto"/> 
    /// </summary>
    /// <param name="users">Список пользователей.</param>
    /// <returns>Список <see cref="UserResponseDto"/>.</returns>
    public static List<UserResponseDto>? ToDetailedResponse(this List<User> users) =>
        users
            .Select(x => x.ToDetailedResponse())
            .ToList();

    /// <summary>
    /// Краткий маппинг <see cref="User"/> в <see cref="GetUsersResponseDto"/> 
    /// </summary>
    /// <param name="user"></param>
    /// <returns><see cref="GetUsersResponseDto"/></returns>
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

    /// <summary>
    /// Краткий маппинг списка <see cref="User"/> в список<see cref="GetUsersResponseDto"/> 
    /// </summary>
    /// <param name="users">Список пользователей.</param>
    /// <returns>Список <see cref="GetUsersResponseDto"/>.</returns>
    public static List<GetUsersResponseDto> ToShortResponse(this List<User> users) =>
        users
            .Select(x => x.ToShortResponse())
            .ToList();

    public static GetUsersQuery ToQuery(this GetUsersRequestDto dto) =>
        new GetUsersQuery(dto.Page, dto.ItemPerPage, dto.SortBy);

    public static AddUserCommand ToCommand(this AddUserRequestDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.FirstName) || string.IsNullOrWhiteSpace(dto.SecondName) ||
            dto.DateOfBirthday is null || string.IsNullOrWhiteSpace(dto.Email) ||
            string.IsNullOrWhiteSpace(dto.Password))
        {
            throw new ArgumentNullException(nameof(dto), "DTO must be validated before conversion");
        }

        return new AddUserCommand(dto.FirstName, dto.SecondName, dto.Patronymic, dto.DateOfBirthday.Value, dto.Email, dto.Password, dto.RoleIds);
    }


    public static AddRolesForUserCommand ToCommand(this AddRolesForUserDto dto, Guid userId) =>
        new AddRolesForUserCommand()
        {
            UserId = userId,
            RoleIds = dto.RoleIds
        };
}