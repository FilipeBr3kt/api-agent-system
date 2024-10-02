using api_sistema_agente.Application.Services.Interface;
using api_sistema_agente.Domain.Entities;
using api_sistema_agente.Domain.Repositories;
using api_sistema_agente.Domain.ViewModel;
using api_sistema_agente.Infrastructure.Services.Interface;
using api_sistema_agente.Infrastructure.Validators;
using Microsoft.AspNetCore.Http;

namespace api_sistema_agente.Application.Services;

public class AuthServices : IAuthServices
{
  private readonly IAuthRepository _repository;
  private readonly ITokenService _tokenService;
  private readonly IPasswordEncryption _passwordEncryption;
  private readonly IMailService _mailService;

  public AuthServices(IAuthRepository repository, ITokenService tokenService, IPasswordEncryption passwordEncryption, IMailService mailService)
  {
    _repository = repository;
    _tokenService = tokenService;
    _passwordEncryption = passwordEncryption;
    _mailService = mailService;
  }

  public async Task<IResult> Login(AuthLoginViewModel model, CancellationToken cancellationToken)
  {
    var validator = new AuthLoginValidator();
    var resultValidator = validator.Validate(model);

    if (!resultValidator.IsValid)
    {
      var errors = resultValidator.Errors.Select(x => x.ErrorMessage).ToList();
      return Results.BadRequest(new { message = errors });
    }

    Auth? user = await _repository.FindUserByName(model.Login!, cancellationToken);

    if (user == null)
    {
      return Results.BadRequest(new { message = "This username does not exist" });
    }

    if (!_passwordEncryption.Verify(model.Password!, user.Password!))
    {
      return Results.BadRequest(new
      {
        message = "Invalid password"
      });
    }

    string token = _tokenService.GenerateToken<Auth>(user, null);
    string refreshToken = _tokenService.GenerateToken<Auth>(user, DateTime.UtcNow.AddDays(7));

    return Results.Ok(new
    {
      token,
      refreshToken
    });
  }

  public Task<IResult> ChangePassword(AuthChangePasswordViewModel model)
  {
    throw new NotImplementedException();
  }

  public IResult RefreshToken(HttpContext httpContext)
  {
    var token = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

    if (string.IsNullOrEmpty(token))
    {
      return Results.Unauthorized();
    }

    var decodedToken = _tokenService.DecodeToken<Auth>(token);

    if (decodedToken == null)
    {
      return Results.Unauthorized();
    }

    string newToken = _tokenService.GenerateToken<Auth>(decodedToken, null);
    string refreshToken = _tokenService.GenerateToken<Auth>(decodedToken, DateTime.UtcNow.AddDays(7));

    return Results.Ok(new
    {
      token = newToken,
      refreshToken
    });
  }

  public async Task<IResult> ResetPassword(AuthResetPasswordViewModel model, CancellationToken token)
  {
    var validator = new AuthResetPasswordValidator();
    var resultValidator = validator.Validate(model);

    if (!resultValidator.IsValid)
    {
      var errors = resultValidator.Errors.Select(x => x.ErrorMessage).ToList();
      return Results.BadRequest(new { message = errors });
    }

    var user = await _repository.FindUserByMail(model.Mail!, token);
    if (user == null)
    {
      return Results.BadRequest(new { message = "This mail does not exist" });
    }


    _mailService.SendMail(user.Mail!, "Password Recovery", $"Hello {user.Login}");

    return Results.Ok(new { message = "The email was sent successfully" });
  }

  public async Task<IResult> Register(AuthRegisterViewModel model, CancellationToken token)
  {
    var validator = new AuthRegisterValidator();
    var resultValidator = validator.Validate(model);

    if (!resultValidator.IsValid)
    {
      var errors = resultValidator.Errors.Select(x => x.ErrorMessage).ToList();
      return Results.BadRequest(new { message = errors });
    }

    var existsUser = await _repository.FindUserByName(model.Login!, token);
    if (existsUser != null)
    {
      return Results.BadRequest(new { message = "This username already exists" });
    }

    var existsMail = await _repository.FindUserByMail(model.Mail!, token);
    if (existsMail != null)
    {
      return Results.BadRequest(new { message = "This mail already exists" });
    }

    var encryptedPassword = _passwordEncryption.Hash(model.Password!);
    Auth registerUser = new Auth
    {
      Login = model.Login,
      Password = encryptedPassword,
      Mail = model.Mail,
    };

    var user = await _repository.CreateUser(registerUser, token);
    return Results.Ok(user);
  }
}