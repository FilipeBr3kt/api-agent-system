namespace api_sistema_agente.Infrastructure.Services.Interface;

public interface IMailService
{
  void SendMail(string to, string subject, string body);
}