namespace api_sistema_agente.Domain.ViewModel;

public class AuthLoginViewModel
{
  public string? Login { get; set; }
  public string? Password { get; set; }
}

public class AuthResetPasswordViewModel
{
  public string? Mail { get; set; }
}

public class AuthChangePasswordViewModel
{
  public string? Password { get; set; }
  public string? Code { get; set; }
}

public class AuthRegisterViewModel
{
  public string? Login { get; set; }
  public string? Password { get; set; }
  public string? Mail { get; set; }
}