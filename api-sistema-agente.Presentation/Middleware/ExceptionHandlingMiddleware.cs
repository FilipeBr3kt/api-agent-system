using System.Text.Json;
using api_sistema_agente.Domain.Exceptions;
using api_sistema_agente.Domain.ViewModel;

namespace api_sistema_agente.Presentation.Middleware;

public class ExceptionHandlingMiddleware
{
  private readonly RequestDelegate _next;

  public ExceptionHandlingMiddleware(RequestDelegate next)
  {
    _next = next;
  }

  public async Task Invoke(HttpContext context)
  {
    try
    {
      await _next(context);
    }
    catch (Exception ex)
    {
      await HandleExceptionAsync(context, ex);
    }
  }

  public static Task HandleExceptionAsync(HttpContext context, Exception ex)
  {
    context.Response.ContentType = "application/json";
    context.Response.StatusCode = ex switch
    {
      ValidationException => StatusCodes.Status400BadRequest,
      InvalidPasswordException => StatusCodes.Status400BadRequest,
      NotFoundException => StatusCodes.Status404NotFound,
      UnauthorizedException => StatusCodes.Status401Unauthorized,
      UserExistsException => StatusCodes.Status409Conflict,
      DatabaseSaveException => StatusCodes.Status500InternalServerError,
      _ => throw new NotImplementedException(),
    };

    var response = new ApiResponse<object>
    {
      Success = false,
      Data = null,
      Errors = ex is ValidationException validation ? validation.Errors : null,
      Message = ex is ValidationException ? "Invalid parameters" : ex.Message
    };

    var jsonResponse = JsonSerializer.Serialize(response);
    return context.Response.WriteAsync(jsonResponse);
  }
}