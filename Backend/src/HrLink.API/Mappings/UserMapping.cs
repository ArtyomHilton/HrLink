using HrLink.API.DTOs.Users;
using HrLink.Application.DTOs;
using HrLink.Application.UseCases.UserUseCases.AddRolesForUser;
using HrLink.Application.UseCases.UserUseCases.AddUser;
using HrLink.Application.UseCases.UserUseCases.ChangePassword;
using HrLink.Application.UseCases.UserUseCases.GetUsers;
using HrLink.Domain.Entities;

namespace HrLink.API.Mappings;

/// <summary>
/// Мапперы для <see cref="User"/>.
/// </summary>
public static class UserMapping
{
    /// <summary>
    /// Маппинг <see cref="User"/> в <see cref="UserDetailResponse"/> 
    /// </summary>
    /// <param name="userDetailData"></param>
    /// <returns><see cref="UserDetailResponse"/></returns>
    public static UserDetailResponse ToResponse(this UserDetailDataResponse userDetailData) => 
        new UserDetailResponse(userDetailData.Id, userDetailData.FirstName, userDetailData.SecondName, userDetailData.Patronymic, userDetailData.DateOfBirthday.ToString("dd.MM.yyyy"), userDetailData.Email, userDetailData.Employee?.ToResponse());
    

    /// <summary>
    /// Краткий маппинг <see cref="User"/> в <see cref="UserShortResponse"/> 
    /// </summary>
    /// <param name="user"></param>
    /// <returns><see cref="UserShortResponse"/></returns>
    public static UserShortResponse ToResponse(this UserShortDataResponse user) =>
        new UserShortResponse(user.Id,user.FirstName,user.SecondName, user.Patronymic, user.IsEmployee);

    /// <summary>
    /// Краткий маппинг списка <see cref="User"/> в список<see cref="UserShortResponse"/> 
    /// </summary>
    /// <param name="users">Список пользователей.</param>
    /// <returns>Список <see cref="UserShortResponse"/>.</returns>
    public static List<UserShortResponse> ToResponse(this List<UserShortDataResponse>? users) =>
        users?.Select(x => x.ToResponse())
            .ToList() 
        ?? new List<UserShortResponse>();

    public static GetUsersQuery ToQuery(this GetUsersRequestDto dto) =>
        new GetUsersQuery(dto.Page, dto.ItemPerPage, dto.SortBy);

    public static AddUserCommand ToCommand(this AddUserRequest dto) =>
        new AddUserCommand(dto.FirstName, dto.SecondName, dto.Patronymic, dto.DateOfBirthday, dto.Email, dto.Password, dto.RoleIds);
    
    public static AddRolesForUserCommand ToCommand(this AddRolesForUserDto dto, Guid userId) =>
        new AddRolesForUserCommand()
        {
            UserId = userId,
            RoleIds = dto.RoleIds
        };

    public static ChangeUserPasswordCommand ToCommand(this ChangeUserPasswordRequestDto dto, Guid userId) =>
        new ChangeUserPasswordCommand(userId, dto.Password, dto.NewPassword);
}