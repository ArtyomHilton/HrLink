using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace HrLink.API.Middlewares;

/// <summary>
/// Глобальная обратка исключений.
/// </summary>
public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (TimeoutException exception)
        {
            var problemDetails = new ProblemDetails()
            {
                Title = "Request Timeout",
                Status = StatusCodes.Status408RequestTimeout,
                Detail = exception.Message
            };

            await ModifyHeaders(context, problemDetails);
        }
        catch (ParseException exception)
        {
            var problemDetails = new ProblemDetails()
            {
                Title = "Parse Error",
                Status = StatusCodes.Status400BadRequest,
                Detail = exception.Message
            };

            await ModifyHeaders(context, problemDetails);
        }
        catch (SmtpCommandException exception)
        {
            var problemDetails = new ProblemDetails()
            {
                Title = "Smtp Error",
                Status = StatusCodes.Status400BadRequest,
                Detail = exception.Message
            };

            await ModifyHeaders(context, problemDetails);
        }
        catch (Exception exception)
        {
            var problemDetails = new ProblemDetails()
            {
                Title = "Internal Server Error",
                Detail = exception.Message,
                Status = StatusCodes.Status500InternalServerError
            };

            await ModifyHeaders(context, problemDetails);
        }
    }

    /// <summary>
    /// Модификация заголовков
    /// </summary>
    /// <param name="context">Текущий <see cref="HttpContext"/>.</param>
    /// <param name="problemDetails">Детали проблемы <see cref="ProblemDetails"/>.</param>
    private async Task ModifyHeaders(HttpContext context, ProblemDetails problemDetails)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(problemDetails, CancellationToken.None);
    }
}

public static class GlobalExceptionHandlerMiddlewareExtensions
{
    /// <summary>
    /// Использовать глобальный обработчик исключений.
    /// </summary>
    /// <param name="builder"><see cref="IApplicationBuilder"/></param>
    /// <returns>Модифицированный <see cref="IApplicationBuilder"/></returns>
    public static IApplicationBuilder UseGlobalExceptionHandlerMiddleware(this IApplicationBuilder builder) =>
        builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
}