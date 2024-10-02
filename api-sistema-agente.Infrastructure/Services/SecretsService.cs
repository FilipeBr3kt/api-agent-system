using api_sistema_agente.Infrastructure.Constants;
using api_sistema_agente.Infrastructure.Services.Interface;
using Microsoft.Extensions.Options;

namespace api_sistema_agente.Infrastructure.Services;

public class SecretsService : ISecretsService
{
  private readonly SecretsApi _secretsApi;

  public SecretsService(IOptions<SecretsApi> secretsApi)
  {
    _secretsApi = secretsApi.Value;
  }

  public string GetSecretKey()
  {
    return _secretsApi.SecretKey;
  }

  public string GetConnectionString()
  {
    return _secretsApi.ConnectionString;
  }

  public string GetMailHost()
  {
    return _secretsApi.Mail.Host;
  }

  public string GetMailEmail()
  {
    return _secretsApi.Mail.Email;
  }

  public string GetMailPassword()
  {
    return _secretsApi.Mail.Password;
  }

  public string GetMailServer()
  {
    return _secretsApi.Mail.Server;
  }
}
