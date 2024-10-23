namespace api_sistema_agente.Domain.ViewModel;

public class AuthLoginViewModel
{
  public string Login { get; set; } = null!;
  public string Password { get; set; } = null!;
}

public class AuthResetPasswordViewModel
{
  public string Mail { get; set; } = null!;
}

public class AuthChangePasswordViewModel
{
  public string Password { get; set; } = null!;
  public string Code { get; set; } = null!;
}

public class AuthRegisterViewModel
{
  public string Login { get; set; } = null!;
  public string Password { get; set; } = null!;
  public string Mail { get; set; } = null!;
}