namespace HrLink.API.DTOs.Errors;

public record ErrorResponse(int StatusCode, string ErrorMessage, string? ErrorType = null, string? ErrorTarget = null, Dictionary<string, object?>? Metadata = null);