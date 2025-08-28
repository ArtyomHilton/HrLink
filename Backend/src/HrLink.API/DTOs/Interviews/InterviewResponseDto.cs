using HrLink.API.DTOs.Candidates;

namespace HrLink.API.DTOs.Interviews;

public class InterviewResponseDto
{
    public required Guid Id { get; set; }
    public CandidateResponseDto Candidate { get; set; }
}