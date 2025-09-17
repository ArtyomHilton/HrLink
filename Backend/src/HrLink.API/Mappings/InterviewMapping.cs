using HrLink.API.DTOs.Interviews;
using HrLink.Application.UseCases.InterviewUseCases.AddInterview;
using HrLink.Domain.Entities;

namespace HrLink.API.Mappings;

/// <summary>
/// Мапперы для <see cref="Interview"/>.
/// </summary>
public static class InterviewMapping
{
    /// <summary>
    /// Маппит <see cref="Interview"/> в <see cref="InterviewResponseDto"/>.
    /// </summary>
    /// <param name="interview">Собеседование.</param>
    /// <returns><see cref="InterviewResponseDto"/></returns>
    public static InterviewResponseDto ToResponse(this Interview interview)
    {
        return new InterviewResponseDto()
        {
            Id = interview.Id,
            Candidate = interview.Candidate!.ToResponse()
        };
    }

    /// <summary>
    /// Маппит коллекцию <see cref="Interview"/> в коллекцию <see cref="InterviewResponseDto"/>.
    /// </summary>
    /// <param name="interviews">Коллекция <see cref="Interview"/></param>
    /// <returns>Коллекцию <see cref="InterviewResponseDto"/>.</returns>
    public static List<InterviewResponseDto> ToResponse(this ICollection<Interview> interviews)
    {
        return interviews
            .Select(x => x.ToResponse())
            .ToList();
    }

    public static AddInterviewCommand ToCommand(this AddInterviewRequestDto dto)
    {
        if (dto.VacancyId is null || dto.CandidateId is null || 
            dto.EmployeeId is null || dto.InterviewDateTime is null)
        {
            throw new ArgumentNullException(nameof(dto), "DTO must be validated before conversion");
        }

        return new AddInterviewCommand(dto.VacancyId.Value, dto.CandidateId.Value, dto.EmployeeId.Value, dto.InterviewDateTime.Value);
    }
}