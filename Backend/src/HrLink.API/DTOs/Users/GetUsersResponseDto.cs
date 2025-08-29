using HrLink.API.DTOs.Employees;
using HrLink.Domain.Entities;

namespace HrLink.API.DTOs.Users;

public class GetUsersResponseDto
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string SecondName { get; set; }
    public string? Patronymic { get; set; } = string.Empty;
    public GetUsersEmployeeResponseDto? Employee { get; set; }
}