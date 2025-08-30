using HrLink.API.DTOs.Candidates;
using HrLink.Domain.Entities;

namespace HrLink.API.Mappings;

/// <summary>
/// Мапперы для <see cref="Candidate"/>.
/// </summary>
public static class CandidateMapping
{
    /// <summary>
    /// Маппит <see cref="Candidate"/> в <see cref="CandidateResponseDto"/>.
    /// </summary>
    /// <param name="candidate">Кандидат</param>
    /// <returns><see cref="CandidateResponseDto"/></returns>
    public static CandidateResponseDto ToResponse(this Candidate candidate)
    {
        return new CandidateResponseDto()
        {
            Id = candidate.Id,
            FirstName = candidate.FirstName,
            SecondName = candidate.SecondName,
            Patronymic = candidate.Patronymic,
            Email = candidate.Email,
            PhoneNumber = candidate.PhoneNumber
        };
    }
}