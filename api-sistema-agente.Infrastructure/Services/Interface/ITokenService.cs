namespace api_sistema_agente.Infrastructure.Services.Interface;

public interface ITokenService
{
  string GenerateToken<T>(T data, DateTime? expirationTime);
  T? DecodeToken<T>(string token);
}