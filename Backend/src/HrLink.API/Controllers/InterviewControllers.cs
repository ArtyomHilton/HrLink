using HrLink.API.DTOs.Errors;
using HrLink.API.DTOs.Interviews;
using HrLink.API.Mappings;
using HrLink.API.Validators;
using HrLink.Application.Common.Results.Errors;
using HrLink.Application.Interfaces;
using HrLink.Application.UseCases.InterviewUseCases.AddInterview;
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
                InterviewSchedulingConflictError => Conflict(result.Error.ToResponse(StatusCodes.Status409Conflict)),
                not null => BadRequest(result.Error.ToResponse(StatusCodes.Status400BadRequest)),
                _ => StatusCode(StatusCodes.Status500InternalServerError,
                    new ErrorResponse(StatusCodes.Status500InternalServerError,
                        ErrorMessage: "An unexcepted error occured"))
            };
        }

        return Created($"/api/interview/{result.Value!.Id}", result.Value!.ToResponse());
    }
}