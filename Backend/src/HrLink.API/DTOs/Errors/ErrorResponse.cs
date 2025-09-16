using HrLink.Application.Common.Results.Errors;

namespace HrLink.API.DTOs.Errors;

public record ErrorResponse(int StatusCode, string ErrorMessage, string? ErrorType = null, string? ErrorTarget = null);