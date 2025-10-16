using HrLink.API.DTOs.Errors;
using HrLink.API.DTOs.Users;
using HrLink.API.Mappings;
using HrLink.Application.Common.Results.Errors;
using HrLink.Application.UseCases.UserUseCases.AddRolesForUser;
using HrLink.Application.UseCases.UserUseCases.AddUser;
using HrLink.Application.UseCases.UserUseCases.ChangePassword;
using HrLink.Application.UseCases.UserUseCases.GetUserByIdUser;
using HrLink.Application.UseCases.UserUseCases.GetUsers;
using Microsoft.AspNetCore.Mvc;

namespace HrLink.API.Controllers;

[ApiController]
[Route("api/user")]
public class UsersControllers : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(UserDetailResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddUser([FromBody] AddUserRequest dto,
        [FromServices] IAddUserUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.Execute(dto.ToCommand(), cancellationToken);

        if (result.IsFailure)
        {
            return result.Error switch
            {
                IValidateError => BadRequest(result.Error.ToResponse(StatusCodes.Status400BadRequest)),
                _ => StatusCode(StatusCodes.Status500InternalServerError)
            };
        }

        return Created(nameof(GetUserById), result.Value);
    }

    [HttpPost("{id:guid}/roles/add")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddRolesForUser(Guid id,
        [FromBody] AddRolesForUserDto dto,
        [FromServices] IAddRolesForUserUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.Execute(dto.ToCommand(id), cancellationToken);

        if (result.IsFailure)
        {
            return result.Error switch
            {
                IValidateError => BadRequest(result.Error.ToResponse(StatusCodes.Status400BadRequest)),
                _ => StatusCode(StatusCodes.Status500InternalServerError)
            };
        }

        return NoContent();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(UserDetailResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserById(Guid id,
        [FromServices] IGetUserByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.Execute(new GetUserByIdQuery(id), cancellationToken);

        if (result.IsFailure)
        {
            return result.Error switch
            {
                INotFoundError => NotFound(result.Error.ToResponse(StatusCodes.Status404NotFound)),
                _ => StatusCode(StatusCodes.Status500InternalServerError)
            };
        }

        return Ok(result.Value!.ToResponse());
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<UserShortResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsers([FromQuery] GetUsersRequestDto requestDto,
        IGetUsersUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.Execute(requestDto.ToQuery(), cancellationToken);

        return Ok(result.Value.ToResponse());
    }

    [HttpPut("/{id:guid}/change-password")]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangePasswordUser(Guid id,
        [FromBody] ChangeUserPasswordRequestDto dto,
        [FromServices] IChangeUserPasswordUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.Execute(dto.ToCommand(id), cancellationToken);

        if (result.IsFailure)
        {
            return result.Error switch
            {
                INotFoundError => NotFound(result.Error.ToResponse(StatusCodes.Status404NotFound)),
                IValidateError => BadRequest(result.Error.ToResponse(StatusCodes.Status400BadRequest)),
                _ => StatusCode(StatusCodes.Status500InternalServerError,
                    result.Error?.ToResponse(StatusCodes.Status500InternalServerError))
            };
        }

        return Ok();
    }
}