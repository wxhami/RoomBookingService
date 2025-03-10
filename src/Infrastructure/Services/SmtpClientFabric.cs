using System.Net;
using System.Net.Mail;
using Application.Common.Interfaces;
using Application.Common.Options;
using Microsoft.Extensions.Options;

namespace Client.Services;

public class SmtpClientFabric(IOptions<EmailOptions> options) : ISmtpClientFabric
{
    public SmtpClient Create()
    {
        return new SmtpClient(options.Value.SmtpHost, options.Value.SmtpPort)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(options.Value.MailSender, options.Value.Password)
        };
    }
}