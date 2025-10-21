using HrLink.API.DTOs.Candidates;

namespace HrLink.API.DTOs.Interviews;

public record GetInterviewsByDateResponse(InterviewShortResponse Interview, CandidateShortResponse Candidate);