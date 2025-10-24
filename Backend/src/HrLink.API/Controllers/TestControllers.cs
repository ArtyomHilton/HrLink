using HrLink.API.DTOs.Test;
using HrLink.Application.Common.Results;
using HrLink.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HrLink.API.Controllers;

[ApiController]
[Route("api/test")]
public class TestControllers : ControllerBase
{
    [HttpPost]
    [Route("/email")]
    public async Task<IActionResult> SendEmail([FromBody] EmailRequest request, 
        IEmailService emailService,
        CancellationToken cancellationToken)
    {
        await emailService.SendMessage(request.Email, "Тест", "Это тестовое сообщение", cancellationToken);
        
        return Ok();
    }

    [HttpPost]
    [Route("/token")]
    public async Task<IActionResult> GetToken([FromServices] IJwtProvider provider, 
        CancellationToken cancellationToken)
    {
        var token = provider.GenerateToken();

        return Ok(token);
    }
}