using api_sistema_agente.Domain.ViewModel;
using Microsoft.AspNetCore.Http;

namespace api_sistema_agente.Application.Services.Interface;

public interface IAuthServices
{
  Task<IResult> Login(AuthLoginViewModel model);
  Task<IResult> ResetPassword(AuthResetPasswordViewModel model);
  Task<IResult> ChangePassword(AuthChangePasswordViewModel model);
  IResult RefreshToken(HttpContext httpContext);
  Task<IResult> Register(AuthRegisterViewModel model);
}