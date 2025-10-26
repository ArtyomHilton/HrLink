using HrLink.API.DTOs.Candidates;
using HrLink.Application.DTOs;
using HrLink.Domain.Entities;

namespace HrLink.API.Mappings;

/// <summary>
/// Мапперы для <see cref="Candidate"/>.
/// </summary>
public static class CandidateMapping
{
    /// <summary>
    /// Маппит <see cref="Candidate"/> в <see cref="CandidateResponse"/>.
    /// </summary>
    /// <param name="candidate">Кандидат</param>
    /// <returns><see cref="CandidateResponse"/></returns>
    public static CandidateResponse ToResponse(this Candidate candidate)
    {
        return new CandidateResponse()
        {
            Id = candidate.Id,
            FirstName = candidate.FirstName,
            SecondName = candidate.SecondName,
            Patronymic = candidate.Patronymic,
            Email = candidate.Email,
            PhoneNumber = candidate.PhoneNumber
        };
    }

    public static CandidateShortResponse ToResponse(this CandidateShortDataResponse data) =>
        new CandidateShortResponse(data.Id, data.FirstName, data.SecondName, data.Patronymic, data.Email);
}