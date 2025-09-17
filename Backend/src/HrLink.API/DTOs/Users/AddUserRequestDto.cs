using HrLink.Application.UseCases.UserUseCases.AddUser;
using HrLink.Domain.Entities;

namespace HrLink.API.DTOs.Users;

public record AddUserRequestDto(string? FirstName, string? SecondName, string? Patronymic, DateTime? DateOfBirthday, string? Email, string? Password, List<Guid>? RoleIds);