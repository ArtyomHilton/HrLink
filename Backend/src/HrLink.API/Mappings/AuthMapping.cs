using HrLink.API.DTOs.Auth;
using HrLink.Application.DTOs;
using HrLink.Application.UseCases.AuthUseCase.Login;

namespace HrLink.API.Mappings;

public static class AuthMapping
{
    public static LoginCommand ToCommand(this LoginRequest request) =>
        new LoginCommand(request.Email, request.Password);

    public static LoginResponse ToResponse(this LoginDataResponse data) =>
        new LoginResponse(data.Id, data.FirstName, data.SecondName, data.Patronymic, data.DateOfBirthday, data.Email,
            data.Roles.ToResponse());
}