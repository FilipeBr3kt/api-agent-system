using api_sistema_agente.Application.Services.Interface;
using api_sistema_agente.Domain.ViewModel;
using api_sistema_agente.Presentation.Controllers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
  public Task<IResult> ChangePassword(AuthChangePasswordViewModel model, string? token)
  {
    throw new NotImplementedException();
  }

  [HttpPost("login")]
  [AllowAnonymous]
  public async Task<IResult> Login(AuthLoginViewModel model)
  {
    return await _service.Login(model);
  }

  [HttpGet("refresh-token")]
  [Authorize]
  public IResult RefreshToken()
  {
    return _service.RefreshToken(HttpContext);
  }

  [HttpPost("register")]
  [AllowAnonymous]
  public async Task<IResult> Register(AuthRegisterViewModel model)
  {
    return await _service.Register(model);
  }

  [HttpPost("reset-password")]
  [AllowAnonymous]
  public async Task<IResult> ResetPassword(AuthResetPasswordViewModel model)
  {
    return await _service.ResetPassword(model);
  }
}