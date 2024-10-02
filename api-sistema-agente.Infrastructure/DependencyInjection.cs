using System.Text;
using api_sistema_agente.Domain.Repositories;
using api_sistema_agente.Infrastructure.Config;
using api_sistema_agente.Infrastructure.Constants;
using api_sistema_agente.Infrastructure.Repository;
using api_sistema_agente.Infrastructure.Services.Interface;
using api_sistema_agente.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace api_sistema_agente.Domain;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddScoped<IMailService, MailService>();
    services.AddScoped<ITokenService, TokenService>();
    services.AddScoped<IPasswordEncryption, PasswordEncryption>();

    services.Configure<SecretsApi>(configuration.GetSection("SecretsApi"));
    services.AddSingleton<ISecretsService, SecretsService>();

    var serviceProvider = services.BuildServiceProvider();
    var secretsOptions = serviceProvider.GetRequiredService<IOptions<SecretsApi>>().Value;

    services.AddDbContext<DatabaseContext>(
      options => options.UseSqlServer(
        secretsOptions.ConnectionString
      )
    );

    services.AddAuthentication(x =>
    {
      x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
      x.RequireHttpsMetadata = false;
      x.SaveToken = true;
      x.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretsOptions.SecretKey)),
        ValidateIssuer = false,
        ValidateAudience = false
      };
    });

    services.AddScoped<IPendenciesRepository, PendenciesRepository>();
    services.AddScoped<IAuthRepository, AuthRepository>();
    return services;
  }
}