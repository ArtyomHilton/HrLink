using System.Globalization;
using HrLink.Domain.Entities;

namespace HrLink.Application.Common.Results.Errors;

public class InterviewSchedulingConflictError : IError
{
    public string Message { get; init; }
    public string Target { get; init; }

    public InterviewSchedulingConflictError(DateTime startDate, DateTime endDate)
    {
        Message =
            $"The employee or candidate already has an {nameof(Interview).ToLower()} scheduled " +
            $"from {startDate.ToString("dd.MM.yyyy HH:mm", CultureInfo.CurrentCulture)} to {endDate.ToString("dd.MM.yyyy HH:mm", CultureInfo.CurrentCulture)}";
        Target = nameof(Interview);
    }
}