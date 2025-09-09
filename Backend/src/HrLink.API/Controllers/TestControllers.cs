using HrLink.API.DTOs.Test;
using HrLink.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HrLink.API.Controllers;

[ApiController]
[Route("api/test")]
public class TestControllers : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SendEmail([FromBody] EmailRequest request, IEmailService emailService,
        CancellationToken cancellationToken)
    {
        await emailService.SendMessage(request.Email, "Тест", "Это тестовое сообщение", cancellationToken);

        return Ok();
    }
}