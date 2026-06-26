using System.Net;
using System.Net.Mail;
using BcpRecordNexusActivity.Configuration;

namespace BcpRecordNexusActivity.Io;

public interface IEmailSender
{
    /// <summary>Sends a notification to the configured recipients, optionally attaching a workbook.</summary>
    Task SendAsync(string subject, string body, string? attachmentPath, CancellationToken cancellationToken = default);
}

/// <summary>SMTP sender built on the BCL <see cref="SmtpClient"/>.</summary>
public sealed class SmtpEmailSender : IEmailSender
{
    private readonly SmtpSettings _smtp;
    private readonly IReadOnlyList<string> _recipients;

    public SmtpEmailSender(SmtpSettings smtp, IReadOnlyList<string> recipients)
    {
        _smtp = smtp;
        _recipients = recipients;
    }

    public async Task SendAsync(string subject, string body, string? attachmentPath, CancellationToken cancellationToken = default)
    {
        using var message = new MailMessage
        {
            From = new MailAddress(_smtp.FromAddress, _smtp.FromName ?? _smtp.FromAddress),
            Subject = subject,
            Body = body,
            IsBodyHtml = false
        };
        foreach (var recipient in _recipients)
            message.To.Add(recipient);

        Attachment? attachment = null;
        if (!string.IsNullOrEmpty(attachmentPath) && File.Exists(attachmentPath))
        {
            attachment = new Attachment(attachmentPath);
            message.Attachments.Add(attachment);
        }

        using var client = new SmtpClient(_smtp.Host, _smtp.Port) { EnableSsl = _smtp.EnableSsl };
        if (!string.IsNullOrEmpty(_smtp.Username))
            client.Credentials = new NetworkCredential(_smtp.Username, _smtp.Password);

        try
        {
            await client.SendMailAsync(message, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            attachment?.Dispose();
        }
    }
}
