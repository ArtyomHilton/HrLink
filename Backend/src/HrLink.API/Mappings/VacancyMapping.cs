using HrLink.API.DTOs.Vacancy;
using HrLink.Application.DTOs;

namespace HrLink.API.Mappings;

public static class VacancyMapping
{
    public static VacancyShortResponse ToResponse(this VacancyShortDataResponse data) =>
        new (data.Id, data.Position);
}