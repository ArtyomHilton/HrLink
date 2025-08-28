using HrLink.API.DTOs.Interviews;
using HrLink.Domain.Entities;

namespace HrLink.API.Mappings;

public static class InterviewMapping
{
    public static InterviewResponseDto ToResponse(this Interview interview)
    {
        return new InterviewResponseDto()
        {
            Id = interview.Id,
            Candidate = interview.Candidate!.ToResponse()
        };
    }

    public static List<InterviewResponseDto> ToResponse(this ICollection<Interview> interviews)
    {
        return interviews
            .Select(x => x.ToResponse())
            .ToList();
    }
}