using HrLink.Application.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace HrLink.Email.Services;

public class EmailService : IEmailService
{
    private readonly IOptions<SmtpOptions> _smtpOptions;

    public EmailService(IOptions<SmtpOptions> smtpOptions)
    {
        _smtpOptions = smtpOptions;
    }

    public async Task SendMessage(string recipientEmail, string subject, string bodyText,
        CancellationToken cancellationToken)
    {
        var message = new MimeMessage();

        message.From.Add(MailboxAddress.Parse(_smtpOptions.Value.Username));
        message.To.Add(MailboxAddress.Parse(recipientEmail));

        message.Subject = subject;

        message.Body = new TextPart(TextFormat.Html)
        {
            Text = bodyText
        };

        using var smtpClient = new SmtpClient();
        
        await smtpClient.ConnectAsync(_smtpOptions.Value.Host, _smtpOptions.Value.Port,
            SecureSocketOptions.Auto, cancellationToken);
        await smtpClient.AuthenticateAsync(_smtpOptions.Value.Username, _smtpOptions.Value.Password,
            cancellationToken);
        await smtpClient.SendAsync(message, cancellationToken);
        await smtpClient.DisconnectAsync(true, cancellationToken);
    }
}