using System.Net.Mail;

namespace Application.Common.Interfaces;

public interface ISmtpClientFabric
{
    SmtpClient Create();
}