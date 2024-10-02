using api_sistema_agente.Infrastructure.Services.Interface;

namespace api_sistema_agente.Infrastructure.Services;

public class PasswordEncryption : IPasswordEncryption
{
  public string Hash(string password)
  {
    return BCrypt.Net.BCrypt.HashPassword(password);
  }

  public bool Verify(string password, string hashedPassword)
  {
    return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
  }
}