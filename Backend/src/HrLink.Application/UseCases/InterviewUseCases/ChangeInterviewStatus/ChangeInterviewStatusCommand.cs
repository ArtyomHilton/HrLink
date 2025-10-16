namespace HrLink.Application.UseCases.InterviewUseCases.ChangeInterviewStatus;

public record ChangeInterviewStatusCommand(Guid InterviewId, byte StatusId);