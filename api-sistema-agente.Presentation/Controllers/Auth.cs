using api_sistema_agente.Application.Services.Interface;
using api_sistema_agente.Domain.ViewModel;
using api_sistema_agente.Presentation.Controllers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using api_sistema_agente.Domain.Entities;

namespace api_sistema_agente.Presentation.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase, IAuthController
{
  private readonly IAuthServices _service;

  public AuthController(IAuthServices service)
  {
    _service = service;
  }

  [HttpPost("change-password")]
  [AllowAnonymous]
  public Task<IActionResult> ChangePassword(AuthChangePasswordViewModel model, string? token)
  {
    throw new NotImplementedException();
  }

  [HttpPost("login")]
  [AllowAnonymous]
  public async Task<IActionResult> Login(AuthLoginViewModel model, CancellationToken token)
  {
    var user = new Auth(model.Login, model.Password);
    return await _service.Login(user, token);
  }

  [HttpGet("refresh-token")]
  [Authorize]
  public IActionResult RefreshToken()
  {
    return _service.RefreshToken(HttpContext);
  }

  [HttpPost("register")]
  [AllowAnonymous]
  public async Task<IActionResult> Register(AuthRegisterViewModel model, CancellationToken token)
  {
    var user = new Auth(model.Login, model.Mail, model.Password);
    return await _service.Register(user, token);
  }

  [HttpPost("reset-password")]
  [AllowAnonymous]
  public async Task<IActionResult> ResetPassword(AuthResetPasswordViewModel model, CancellationToken token)
  {
    var user = new Auth(model.Mail, string.Empty);
    return await _service.ResetPassword(user, token);
  }
}