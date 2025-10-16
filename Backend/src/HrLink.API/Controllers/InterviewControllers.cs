using HrLink.API.DTOs.Errors;
using HrLink.API.DTOs.Interviews;
using HrLink.API.Mappings;
using HrLink.Application.Common.Results.Errors;
using HrLink.Application.UseCases.InterviewUseCases.AddInterview;
using HrLink.Application.UseCases.InterviewUseCases.ChangeInterviewStatus;
using Microsoft.AspNetCore.Mvc;

namespace HrLink.API.Controllers;

[ApiController]
[Route("/api/interview")]
public class InterviewControllers : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(InterviewDetailResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddInterview([FromBody] AddInterviewRequestDto dto,
        [FromServices] IAddInterviewUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.Execute(dto.ToCommand(), cancellationToken);

        if (result.IsFailure)
        {
            return result.Error switch
            {
                IValidateError => BadRequest(result.Error.ToResponse(StatusCodes.Status400BadRequest)),
                _ => StatusCode(StatusCodes.Status500InternalServerError,
                    new ErrorResponse(StatusCodes.Status500InternalServerError, "An unexcepted error occured"))
            };
        }

        return Created($"/api/interview/{result.Value!.Id}", result.Value!.ToResponse());
    }

    [HttpPut("/{interviewId:guid}/change-status")]
    [ProducesResponseType(typeof(InterviewDetailResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
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
                INotFoundError => NotFound(result.Error.ToResponse(StatusCodes.Status404NotFound)),
                IValidateError => BadRequest(result.Error.ToResponse(StatusCodes.Status400BadRequest)),
                _ => StatusCode(StatusCodes.Status500InternalServerError,
                    new ErrorResponse(StatusCodes.Status500InternalServerError, "An unexcepted error occured"))
            };
        }

        return Ok();
    }
}