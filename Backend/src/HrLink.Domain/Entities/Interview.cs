namespace HrLink.Domain.Entities;

public class Interview
{
    public required Guid Id { get; set; }
    public required Guid CandidateId { get; set; }
    public Candidate? Candidate { get; set; }
    public required Guid EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public required DateTime InterviewDateTime { get; set; }
}