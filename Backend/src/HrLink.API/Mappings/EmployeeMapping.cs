using HrLink.API.DTOs.Employees;
using HrLink.Domain.Entities;

namespace HrLink.API.Mappings;

/// <summary>
/// Мапперы для <see cref="Employee"/>.
/// </summary>
public static class EmployeeMapping
{
    /// <summary>
    /// Детальный маппинг <see cref="Employee"/> в <see cref="EmployeeResponseDto"/>.
    /// </summary>
    /// <param name="employee">Сотрудник.</param>
    /// <returns><see cref="EmployeeResponseDto"/>.</returns>
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

    /// <summary>
    /// Краткий маппинг <see cref="Employee"/> в <see cref="EmployeeResponseDto"/>.
    /// </summary>
    /// <param name="employee">Сотрудник.</param>
    /// <returns><see cref="EmployeeResponseDto"/>.</returns>
    public static GetUsersEmployeeResponseDto ToShortResponse(this Employee employee)
    {
        return new GetUsersEmployeeResponseDto
        {
            Id = employee.Id,
            Position = employee.Position
        };
    }
}