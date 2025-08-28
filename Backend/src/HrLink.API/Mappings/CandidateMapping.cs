using HrLink.API.DTOs.Candidates;
using HrLink.Domain.Entities;

namespace HrLink.API.Mappings;

public static class CandidateMapping
{
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