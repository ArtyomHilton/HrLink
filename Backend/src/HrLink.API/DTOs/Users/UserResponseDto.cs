using HrLink.API.DTOs.Employees;
using HrLink.Domain.Entities;

namespace HrLink.API.DTOs.Users;

public class UserResponseDto
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string SecondName { get; set; }
    public string? Patronymic { get; set; }
    public required DateTime DateOfBirthday { get; set; }
    public required string Email { get; set; }
    public EmployeeResponseDto? Employee { get; set; }
}