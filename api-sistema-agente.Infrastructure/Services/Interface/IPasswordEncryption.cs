namespace api_sistema_agente.Infrastructure.Services.Interface;

public interface IPasswordEncryption
{
  string Hash(string password);
  bool Verify(string password, string hashedPassword);
}