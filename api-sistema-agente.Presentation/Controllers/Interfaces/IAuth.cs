using api_sistema_agente.Domain.ViewModel;

namespace api_sistema_agente.Presentation.Controllers.Interfaces;

public interface IAuthController
{
  public Task<IResult> Login(AuthLoginViewModel model, CancellationToken token);
  public Task<IResult> ResetPassword(AuthResetPasswordViewModel model, CancellationToken token);
  public Task<IResult> ChangePassword(AuthChangePasswordViewModel model, string? token);
  public IResult RefreshToken();
  public Task<IResult> Register(AuthRegisterViewModel model, CancellationToken token);
}