using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api_sistema_agente.Infrastructure.Services.Interface;
using Microsoft.IdentityModel.Tokens;

namespace api_sistema_agente.Infrastructure.Services;

public class TokenService : ITokenService
{

  private readonly ISecretsService _secrets;

  public TokenService(ISecretsService secrets)
  {
    _secrets = secrets;
  }

  public string GenerateToken<T>(T data, DateTime? expirationTime)
  {
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(_secrets.GetSecretKey());

    var claims = new List<Claim>();
    foreach (var prop in typeof(T).GetProperties())
    {
      var value = prop.GetValue(data)?.ToString();
      if (value != null)
      {
        claims.Add(new Claim(prop.Name, value));
      }
    }

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),
      Expires = expirationTime ?? DateTime.UtcNow.AddHours(2),
      SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };
    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
  }

  public T? DecodeToken<T>(string token)
  {
    var tokenHandler = new JwtSecurityTokenHandler();
    var jwtToken = tokenHandler.ReadJwtToken(token);

    var data = Activator.CreateInstance<T>();
    foreach (var prop in typeof(T).GetProperties())
    {
      var claim = jwtToken.Claims.FirstOrDefault(c => c.Type == prop.Name);
      if (claim != null)
      {
        prop.SetValue(data, Convert.ChangeType(claim.Value, prop.PropertyType));
      }
    }

    return data;
  }
}