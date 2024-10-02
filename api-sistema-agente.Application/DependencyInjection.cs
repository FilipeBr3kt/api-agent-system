using api_sistema_agente.Application.Services;
using api_sistema_agente.Application.Services.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace api_sistema_agente.Application;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddScoped<IPendenciesService, PendenciesService>();
    services.AddScoped<IAuthServices, AuthServices>();

    return services;
  }
}