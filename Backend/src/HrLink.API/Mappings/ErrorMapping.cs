using HrLink.API.DTOs.Errors;
using HrLink.Application.Common.Results.Errors;

namespace HrLink.API.Mappings;

public static class ErrorMapping
{
    public static ErrorResponse ToResponse(this IError error, int statusCode) =>
        new ErrorResponse(statusCode, error.Message, error.GetType().Name, error.Target);
}