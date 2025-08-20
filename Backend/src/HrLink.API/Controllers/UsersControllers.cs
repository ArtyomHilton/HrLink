using HrLink.API.DTOs.Users;
using HrLink.Application.Common.Results.Errors;
using HrLink.Application.UseCases.UserUseCases.AddUser;
using Microsoft.AspNetCore.Mvc;

namespace HrLink.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersControllers : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddUser([FromBody] AddUserRequestDto dto, 
        [FromServices] IAddUserUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.Execute(dto.ToModel(), cancellationToken);

        if (result.IsFailure)
        {
            return result.Error switch
            {
                UserEmailExistError => BadRequest(new
                {
                    Message = result.Error.Messsage,
                    Target = result.Error.Target
                }),
                _ => StatusCode(StatusCodes.Status500InternalServerError)
            };
        }

        return Created();
    }
}