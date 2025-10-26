namespace HrLink.API.DTOs.Employees;

public record GetUsersEmployeeResponse
{
    public required Guid Id { get; set; }
    public required string Position { get; set; }
}