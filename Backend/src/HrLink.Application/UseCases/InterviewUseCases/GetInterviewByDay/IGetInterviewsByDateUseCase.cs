using HrLink.Application.Common.Results;
using HrLink.Application.DTOs;

namespace HrLink.Application.UseCases.InterviewUseCases.GetInterviewByDay;

public interface IGetInterviewsByDateUseCase
{
    public Task<Result<List<GetInterviewByDateDataResponse>>> Execute(GetInterviewsByDateQuery query, CancellationToken cancellationToken);
}