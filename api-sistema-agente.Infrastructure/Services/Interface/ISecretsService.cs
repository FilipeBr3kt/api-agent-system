namespace api_sistema_agente.Infrastructure.Services.Interface;

public interface ISecretsService
{
  string GetSecretKey();
  string GetConnectionString();
  string GetMailHost();
  string GetMailEmail();
  string GetMailPassword();
  string GetMailServer();
}