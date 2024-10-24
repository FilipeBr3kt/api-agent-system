namespace api_sistema_agente.Domain.ViewModel;
public record AuthLoginViewModel(string Login, string Password);
public record AuthResetPasswordViewModel(string Mail);
public record AuthChangePasswordViewModel(string Password, string Code);
public record AuthRegisterViewModel(string Login, string Password, string Mail);
