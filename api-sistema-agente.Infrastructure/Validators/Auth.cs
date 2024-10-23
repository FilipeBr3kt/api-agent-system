using api_sistema_agente.Domain.Entities;
using api_sistema_agente.Domain.ViewModel;
using FluentValidation;

namespace api_sistema_agente.Infrastructure.Validators;

public class AuthLoginValidator : AbstractValidator<Auth>
{
  public AuthLoginValidator()
  {
    RuleFor(x => x.Login).NotEmpty().WithMessage("Login is required");
    RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
  }
}

public class AuthChangePasswordValidator : AbstractValidator<AuthChangePasswordViewModel>
{
  public AuthChangePasswordValidator()
  {
    RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
    RuleFor(x => x.Code).NotEmpty().WithMessage("Code is required");
  }
}

public class AuthResetPasswordValidator : AbstractValidator<Auth>
{
  public AuthResetPasswordValidator()
  {
    RuleFor(x => x.Mail).NotEmpty().WithMessage("Mail is required");
  }
}

public class AuthRegisterValidator : AbstractValidator<Auth>
{
  public AuthRegisterValidator()
  {
    RuleFor(x => x.Login).NotEmpty().WithMessage("Login is required");
    RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
    .Matches(@"[0-9]").WithMessage("Password must contain at least one number")
    .Matches(@"[\W_]").WithMessage("Password must contain at least one special character");
    RuleFor(x => x.Mail).NotEmpty().WithMessage("Mail is required").EmailAddress().WithMessage("Mail is not valid");
  }
}