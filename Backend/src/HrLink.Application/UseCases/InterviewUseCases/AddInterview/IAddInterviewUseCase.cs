using HrLink.Application.Common.Results;
using HrLink.Application.DTOs;
using HrLink.Domain.Entities;

namespace HrLink.Application.UseCases.InterviewUseCases.AddInterview;

public interface IAddInterviewUseCase
{
    Task<Result<InterviewDetailDataResponse?>> Execute(AddInterviewCommand command, CancellationToken cancellationToken);
}