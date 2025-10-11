using HrLink.API.DTOs.Errors;
using HrLink.API.DTOs.Interviews;
using HrLink.API.Mappings;
using HrLink.API.Validators;
using HrLink.Application.Common.Results.Errors;
using HrLink.Application.UseCases.InterviewUseCases.AddInterview;
using HrLink.Application.UseCases.InterviewUseCases.ChangeInterviewStatus;
using HrLink.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HrLink.API.Controllers;

[ApiController]
[Route("/api/interview")]
public class InterviewControllers : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddInterview([FromBody] AddInterviewRequestDto dto,
        [FromServices] IAddInterviewUseCase useCase,
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
                _ => StatusCode(StatusCodes.Status500InternalServerError,
                    new ErrorResponse(StatusCodes.Status500InternalServerError, "An unexcepted error occured"))
            };
        }

        return Created($"/api/interview/{result.Value!.Id}", result.Value!.ToResponse());
    }

    [HttpPut("/{interviewId:guid}/change-status")]
    public async Task<IActionResult> ChangeStatusInterview(Guid interviewId,
        [FromBody] ChangeInterviewStatusDto dto,
        IChangeInterviewStatusUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.Execute(dto.ToCommand(interviewId), cancellationToken);

        if (result.IsFailure)
        {
            return result.Error switch
            {
                NotFoundError<Interview> => NotFound(result.Error.ToResponse(StatusCodes.Status404NotFound)),
                NotFoundError<Status> => NotFound(result.Error.ToResponse(StatusCodes.Status404NotFound)),
                _ => StatusCode(StatusCodes.Status500InternalServerError,
                    new ErrorResponse(StatusCodes.Status500InternalServerError, "An unexcepted error occured"))
            };
        }

        return Ok();
    }
}