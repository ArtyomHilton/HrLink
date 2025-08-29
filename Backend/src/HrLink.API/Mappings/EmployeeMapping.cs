using HrLink.API.DTOs.Employees;
using HrLink.Domain.Entities;

namespace HrLink.API.Mappings;

public static class EmployeeMapping
{
    public static EmployeeResponseDto ToDetailedResponse(this Employee employee)
    {
        return new EmployeeResponseDto()
        {
            Id = employee.Id,
            Position = employee.Position,
            WorkEmail = employee.WorkEmail,
            WorkPhoneNumber = employee.WorkPhoneNumber,
            DateOfEmployment = employee.DateOfEmployment,
            Interview = employee.Interviews?.ToResponse()
        };
    }

    public static GetUsersEmployeeResponseDto ToShortResponse(this Employee employee)
    {
        return new GetUsersEmployeeResponseDto
        {
            Id = employee.Id,
            Position = employee.Position
        };
    }
}