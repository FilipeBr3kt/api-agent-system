using System.Net.Mail;
using api_sistema_agente.Infrastructure.Services.Interface;

namespace api_sistema_agente.Infrastructure.Services;

public class MailService : IMailService
{
  private readonly ISecretsService _secrets;

  public MailService(ISecretsService secrets)
  {
    _secrets = secrets;
  }

  public void SendMail(string to, string subject, string body)
  {
    string from = _secrets.GetMailEmail();
    MailMessage message = new MailMessage(from, to);
    message.Subject = subject;
    message.Body = body;
    SmtpClient client = new SmtpClient(_secrets.GetMailServer(), 587);
    client.EnableSsl = true;
    client.Credentials = new System.Net.NetworkCredential(from, _secrets.GetMailPassword());

    try
    {
      client.Send(message);
    }
    catch (Exception e)
    {
      Console.WriteLine("Exception caught in CreateTestMessage(): {0}", e.ToString());
    }

  }
}