using api_sistema_agente.Domain.Entities;
using api_sistema_agente.Domain.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_sistema_agente.Application.Services.Interface;

public interface IAuthServices
{
  Task<IActionResult> Login(Auth user, CancellationToken cancellationToken);
  Task<IActionResult> ResetPassword(Auth user, CancellationToken token);
  Task<IActionResult> ChangePassword(Auth user);
  IActionResult RefreshToken(HttpContext httpContext);
  Task<IActionResult> Register(Auth user, CancellationToken token);
}