using api_sistema_agente.Presentation.Controllers;
using api_sistema_agente.Presentation.Controllers.Interfaces;

namespace api_sistema_agente.Presentation;

public static class DependencyInjection
{
  public static IServiceCollection AddPresentation(this IServiceCollection services)
  {
    services.AddScoped<IAuthController, AuthController>();
    return services;
  }
}