using HrLink.Domain.Entities;
using Status = HrLink.Domain.Enums.Status;

namespace HrLink.Application.UseCases.InterviewUseCases.AddInterview;

public record AddInterviewCommand
{
    public Guid VacancyId { get; set; }
    public Guid CandidateId { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime InterviewDateTime { get; set; }

    public AddInterviewCommand(Guid vacancyId, Guid candidateId, Guid employeeId, DateTime interviewDateTime)
    {
        VacancyId = vacancyId;
        CandidateId = candidateId;
        EmployeeId = employeeId;
        InterviewDateTime = interviewDateTime;
    }

    public Interview ToEntity() => new Interview()
    {
        Id = Guid.NewGuid(),
        VacancyId = this.VacancyId,
        CandidateId = this.CandidateId,
        EmployeeId = this.EmployeeId,
        InterviewDateTime = this.InterviewDateTime,
        StatusId = (byte)Status.Wait
    };
}