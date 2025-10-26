using HrLink.API.DTOs.Auth;
using HrLink.API.DTOs.Errors;
using HrLink.API.Mappings;
using HrLink.Application.Common.Results.Errors;
using HrLink.Application.Interfaces;
using HrLink.Application.UseCases.AuthUseCase.Login;
using Microsoft.AspNetCore.Mvc;

namespace HrLink.API.Controllers;

[ApiController]
[Route("/api/auth/")]
public class AuthControllers : ControllerBase
{
    [HttpPost]
    [Route("/login")]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, 
        [FromServices] ILoginUseCase useCase,
        [FromServices] IJwtProvider jwtProvider,
        CancellationToken cancellationToken)
    {
        var result = await useCase.Execute(request.ToCommand(), cancellationToken);

        if (result.IsFailure)
        {
            return result.Error switch
            {
                INotFoundError => NotFound(result.Error.ToResponse(StatusCodes.Status404NotFound)),
                UnAuthorizedError => Unauthorized(result.Error.ToResponse(StatusCodes.Status401Unauthorized)),
                _ => StatusCode(StatusCodes.Status500InternalServerError,
                    result.Error.ToResponse(StatusCodes.Status500InternalServerError))
            };
        }

        var token = jwtProvider.GenerateToken(result.Value!);

        Response.Cookies.Append("X-TASTY-X", token);

        return Ok(result.Value!.ToResponse());
    }
}