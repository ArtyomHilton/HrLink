using HrLink.API.DTOs.Users;
using HrLink.API.Mappings;
using HrLink.API.Validators;
using HrLink.Application.Common.Results.Errors;
using HrLink.Application.UseCases.UserUseCases.AddRolesForUser;
using HrLink.Application.UseCases.UserUseCases.AddUser;
using HrLink.Application.UseCases.UserUseCases.ChangePassword;
using HrLink.Application.UseCases.UserUseCases.GetUserByIdUser;
using HrLink.Application.UseCases.UserUseCases.GetUsers;
using HrLink.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HrLink.API.Controllers;

[ApiController]
[Route("api/user")]
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
        var validateResult = dto.Validate();

        if (validateResult.IsFailure)
        {
            return BadRequest(validateResult.Error!.ToResponse(StatusCodes.Status400BadRequest));
        }

        var result = await useCase.Execute(dto.ToCommand(), cancellationToken);

        if (result.IsFailure)
        {
            return result.Error switch
            {
                not null => BadRequest(result.Error.ToResponse(StatusCodes.Status400BadRequest)),
                _ => StatusCode(StatusCodes.Status500InternalServerError)
            };
        }

        return Created();
    }

    [HttpPost("{userId:guid}/roles/add")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddRolesForUser(Guid userId,
        [FromBody] AddRolesForUserDto dto,
        [FromServices] IAddRolesForUserUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.Execute(dto.ToCommand(userId), cancellationToken);

        if (result.IsFailure)
        {
            return result.Error switch
            {
                not null => BadRequest(result.Error.ToResponse(StatusCodes.Status400BadRequest)),
                _ => StatusCode(StatusCodes.Status500InternalServerError)
            };
        }

        return NoContent();
    }

    [HttpGet("{userId:guid}")]
    [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserById(Guid userId,
        [FromServices] IGetUserByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.Execute(new GetUserByIdQuery() { Id = userId }, cancellationToken);

        if (result.IsFailure)
        {
            return result.Error switch
            {
                NotFoundError<User> => NotFound(result.Error.ToResponse(StatusCodes.Status404NotFound)),
                _ => StatusCode(StatusCodes.Status500InternalServerError)
            };
        }

        return Ok(result.Value!.ToDetailedResponse());
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetUsersResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsers([FromQuery] GetUsersRequestDto requestDto,
        IGetUsersUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.Execute(requestDto.ToQuery(), cancellationToken);

        return Ok(result.Value?.ToShortResponse());
    }

    [HttpPut("/{userId:guid}/change-password")]
    public async Task<IActionResult> ChangePasswordUser(Guid userId,
        [FromBody] ChangeUserPasswordRequestDto dto,
        [FromServices] IChangeUserPasswordUseCase useCase,
        CancellationToken cancellationToken)
    {
        var validateResult = dto.Validate();

        if (validateResult.IsFailure)
        {
            return BadRequest(validateResult.Error!.ToResponse(StatusCodes.Status400BadRequest));
        }

        var result = await useCase.Execute(dto.ToCommand(userId), cancellationToken);

        if (result.IsFailure)
        {
            return result.Error switch
            {
                NotFoundError<User> => NotFound(result.Error.ToResponse(StatusCodes.Status404NotFound)),
                ValidateError => BadRequest(result.Error.ToResponse(StatusCodes.Status400BadRequest)),
                _ => StatusCode(StatusCodes.Status500InternalServerError,
                    result.Error?.ToResponse(StatusCodes.Status500InternalServerError))
            };
        }

        return Ok();
    }
}