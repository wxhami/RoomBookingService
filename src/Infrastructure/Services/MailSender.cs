using System.Net.Mail;
using Application.Common.Interfaces;
using Application.Common.Options;
using Microsoft.Extensions.Options;

namespace Client.Services;

public class MailSender(ISmtpClientFabric fabric, IOptions<EmailOptions> options) : IMailSender
{
    public async Task SendMessageAsync(string mailRecipient, string subject, string message)
    {
        using var client = fabric.Create();

        await client.SendMailAsync(new MailMessage(from: options.Value.MailSender, to: mailRecipient, subject,
            message));
    }
}