using api_sistema_agente.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace api_sistema_agente.Presentation.Controllers.Interfaces;

public interface IAuthController
{
  public Task<IActionResult> Login(AuthLoginViewModel model, CancellationToken token);
  public Task<IActionResult> ResetPassword(AuthResetPasswordViewModel model, CancellationToken token);
  public Task<IActionResult> ChangePassword(AuthChangePasswordViewModel model, string? token);
  public IActionResult RefreshToken();
  public Task<IActionResult> Register(AuthRegisterViewModel model, CancellationToken token);
}