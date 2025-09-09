namespace HrLink.Application.Interfaces;

public interface IEmailService
{
    Task SendMessage(string recipientEmail, string subject, string bodyText, CancellationToken cancellationToken);
}