using HrLink.API.DTOs.Employees;
using HrLink.Application.DTOs;
using HrLink.Domain.Entities;

namespace HrLink.API.Mappings;

/// <summary>
/// Мапперы для <see cref="Employee"/>.
/// </summary>
public static class EmployeeMapping
{
    /// <summary>
    /// Детальный маппинг <see cref="Employee"/> в <see cref="EmployeeResponse"/>.
    /// </summary>
    /// <param name="detailData">Сотрудник.</param>
    /// <returns><see cref="EmployeeResponse"/>.</returns>
    public static EmployeeResponse ToResponse(this EmployeeDetailDataResponse detailData) => 
        new (detailData.Id, detailData.Position, detailData.WorkEmail, detailData.WorkPhoneNumber, detailData.DateOfEmployment.ToString("dd.MM.yyyy"), detailData.Interviews.ToResponse());

    public static EmployeeShortResponse ToResponse(this EmployeeShortDataResponse data) =>
        new EmployeeShortResponse(data.Id, data.FirstName, data.SecondName, data.Patronymic);
    
    /// <summary>
    /// Краткий маппинг <see cref="Employee"/> в <see cref="EmployeeResponse"/>.
    /// </summary>
    /// <param name="employee">Сотрудник.</param>
    /// <returns><see cref="EmployeeResponse"/>.</returns>
    public static GetUsersEmployeeResponse ToShortResponse(this Employee employee)
    {
        return new GetUsersEmployeeResponse
        {
            Id = employee.Id,
            Position = employee.Position
        };
    }
}