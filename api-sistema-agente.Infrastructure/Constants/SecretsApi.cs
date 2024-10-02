namespace api_sistema_agente.Infrastructure.Constants;

public class SecretsApi
{
  public string SecretKey { get; set; } = null!;
  public string ConnectionString { get; set; } = null!;
  public MailSettings Mail { get; set; } = new MailSettings();

  public class MailSettings
  {
    public string Host { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Server { get; set; } = null!;
  }
}
