using api_sistema_agente.Application.Services.Interface;
using api_sistema_agente.Domain.Entities;
using api_sistema_agente.Domain.Exceptions;
using api_sistema_agente.Domain.Repositories;
using api_sistema_agente.Domain.ViewModel;
using api_sistema_agente.Infrastructure.Services.Interface;
using api_sistema_agente.Infrastructure.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_sistema_agente.Application.Services;

public class AuthServices : IAuthServices
{
  private readonly IAuthRepository _repository;
  private readonly ITokenService _tokenService;
  private readonly IMailService _mailService;

  public AuthServices(IAuthRepository repository, ITokenService tokenService, IMailService mailService)
  {
    _repository = repository;
    _tokenService = tokenService;
    _mailService = mailService;
  }

  public async Task<IActionResult> Login(Auth user, CancellationToken cancellationToken)
  {
    var validator = new AuthLoginValidator();
    var resultValidator = validator.Validate(user);

    if (!resultValidator.IsValid)
    {
      var errors = resultValidator.Errors.Select(x => x.ErrorMessage).ToList();
      throw new ValidationException(errors);
    }

    Auth? userExists = await _repository.FindUserByName(user.Login!, cancellationToken);

    if (userExists == null)
    {
      throw new UserExistsException("This username does not exist");
    }

    if (!userExists.VerifyPassword(user.Password!))
    {
      throw new InvalidPasswordException();
    }

    string token = _tokenService.GenerateToken<Auth>(user, null);
    string refreshToken = _tokenService.GenerateToken<Auth>(user, DateTime.UtcNow.AddDays(7));

    var response = new { token, refreshToken };
    return new OkObjectResult(new ApiResponse<object>
    {
      Success = true,
      Data = response,
    });
  }

  public Task<IActionResult> ChangePassword(Auth user)
  {
    throw new NotImplementedException();
  }

  public IActionResult RefreshToken(HttpContext httpContext)
  {
    var token = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

    if (string.IsNullOrEmpty(token))
    {
      throw new UnauthorizedException("Token not found");
    }

    var decodedToken = _tokenService.DecodeToken<Auth>(token);

    if (decodedToken == null)
    {
      throw new UnauthorizedException("Invalid token");
    }

    string newToken = _tokenService.GenerateToken<Auth>(decodedToken, null);
    string refreshToken = _tokenService.GenerateToken<Auth>(decodedToken, DateTime.UtcNow.AddDays(7));

    var response = new { token = newToken, refreshToken };
    return new OkObjectResult(new ApiResponse<object>
    {
      Success = true,
      Data = response
    });
  }

  public async Task<IActionResult> ResetPassword(Auth user, CancellationToken token)
  {
    var validator = new AuthResetPasswordValidator();
    var resultValidator = validator.Validate(user);

    if (!resultValidator.IsValid)
    {
      var errors = resultValidator.Errors.Select(x => x.ErrorMessage).ToList();
      throw new ValidationException(errors);
    }

    var userExists = await _repository.FindUserByMail(user.Mail!, token);
    if (userExists == null)
    {
      throw new UserExistsException("This mail does not exist");
    }


    _mailService.SendMail(user.Mail!, "Password Recovery", $"Hello {user.Login}");

    return new OkObjectResult(new ApiResponse<object>
    {
      Success = true,
      Data = new { Message = "The email was sent successfully" }
    });
  }

  public async Task<IActionResult> Register(Auth user, CancellationToken token)
  {
    var validator = new AuthRegisterValidator();
    var resultValidator = validator.Validate(user);

    if (!resultValidator.IsValid)
    {
      var errors = resultValidator.Errors.Select(x => x.ErrorMessage).ToList();
      throw new ValidationException(errors);
    }

    var existsUser = await _repository.FindUserByName(user.Login!, token);
    if (existsUser != null)
    {
      throw new UserExistsException();
    }

    var existsMail = await _repository.FindUserByMail(user.Mail!, token);
    if (existsMail != null)
    {
      throw new UserExistsException("This mail already exists");
    }

    var assertUser = await _repository.SaveUser(user, token);
    if (assertUser == null)
    {
      throw new DatabaseSaveException();
    }

    return new OkObjectResult(new ApiResponse<object>
    {
      Success = true,
      Data = new { message = "User created successfully" }
    });
  }
}