using HrLink.API.DTOs.Interviews;
using HrLink.Application.DTOs;
using HrLink.Application.UseCases.InterviewUseCases.AddInterview;
using HrLink.Application.UseCases.InterviewUseCases.ChangeInterviewStatus;
using HrLink.Application.UseCases.InterviewUseCases.GetInterviewByDay;
using HrLink.Domain.Entities;

namespace HrLink.API.Mappings;

/// <summary>
/// Мапперы для <see cref="Interview"/>.
/// </summary>
public static class InterviewMapping
{
    /// <summary>
    /// Маппит <see cref="Interview"/> в <see cref="InterviewShortResponse"/>.
    /// </summary>
    /// <param name="data">Собеседование.</param>
    /// <returns><see cref="InterviewShortResponse"/></returns>
    public static InterviewShortResponse ToResponse(this InterviewShortDataResponse data) =>
        new(data.Id, data.VacancyName, data.InterviewDateTime.ToString("HH:mm dd.MM.yyyy"), data.Status);

    /// <summary>
    /// Маппит коллекцию <see cref="Interview"/> в коллекцию <see cref="InterviewShortResponse"/>.
    /// </summary>
    /// <param name="interviews">Коллекция <see cref="Interview"/></param>
    /// <returns>Коллекцию <see cref="InterviewShortResponse"/>.</returns>
    public static List<InterviewShortResponse> ToResponse(this ICollection<InterviewShortDataResponse> interviews)
    {
        return interviews
            .Select(x => x.ToResponse())
            .ToList();
    }

    public static InterviewDetailResponse ToResponse(this InterviewDetailDataResponse data) =>
        new InterviewDetailResponse(data.Id, data.Vacancy.ToResponse(), data.Candidate.ToResponse(),
            data.Employee.ToResponse(), data.InterviewDateTime.ToString("HH:mm dd.MM.yyyy"), data.Status);

    public static AddInterviewCommand ToCommand(this AddInterviewRequestDto dto) =>
        new AddInterviewCommand(dto.VacancyId, dto.CandidateId, dto.EmployeeId, dto.InterviewDateTime);

    public static ChangeInterviewStatusCommand ToCommand(this ChangeInterviewStatusDto dto, Guid interviewId) =>
        new ChangeInterviewStatusCommand(interviewId, dto.StatusId);

    public static GetInterviewsByDateQuery ToQuery(this GetInterviewsByDateRequest request) =>
        new GetInterviewsByDateQuery(request.InterviewDate);

    public static List<GetInterviewsByDateResponse> ToResponse(this List<GetInterviewByDateDataResponse> response) =>
        response.Select(x => x.ToResponse())
            .ToList();

    public static GetInterviewsByDateResponse ToResponse(this GetInterviewByDateDataResponse response) =>
        new GetInterviewsByDateResponse(response.Interview.ToResponse(), response.Candidate.ToResponse());
}