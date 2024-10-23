using api_sistema_agente.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace api_sistema_agente.Presentation.Middleware;

public class ExceptionToProblemDetailsHandler : Microsoft.AspNetCore.Diagnostics.IExceptionHandler
{
	private static readonly Dictionary<Type, (int StatusCode, string Type, string Title)> ExceptionDetails = new()
		{
				{ typeof(DatabaseSaveException), (StatusCodes.Status500InternalServerError, "https://datatracker.ietf.org/doc/html/rfc9110#name-500-internal-server-error", "Internal Server Error") },
				{ typeof(InvalidPasswordException), (StatusCodes.Status400BadRequest, "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.1", "Bad Request") },
				{ typeof(NotFoundException), (StatusCodes.Status404NotFound, "https://datatracker.ietf.org/doc/html/rfc9110#name-404-not-found", "Not Found") },
				{ typeof(UnauthorizedException), (StatusCodes.Status401Unauthorized, "https://datatracker.ietf.org/doc/html/rfc9110#name-401-unauthorized", "Unauthorized") },
				{ typeof(UserExistsException), (StatusCodes.Status400BadRequest, "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.1", "Bad Request") },
				{ typeof(ValidationException), (StatusCodes.Status400BadRequest, "https://datatracker.ietf.org/doc/html/rfc9110#section-15.5.1", "Bad Request") }
		};

	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
	{
		var exceptionType = exception.GetType();
		var (statusCode, type, title) = ExceptionDetails.GetValueOrDefault(exceptionType,
				(StatusCodes.Status500InternalServerError,
				"https://datatracker.ietf.org/doc/html/rfc9110#name-500-internal-server-error",
				"Internal Server Error"));

		var problemDetails = new ProblemDetails
		{
			Title = title,
			Detail = exception.Message,
			Type = type,
			Status = statusCode,
		};

		httpContext.Response.StatusCode = statusCode;
		httpContext.Response.ContentType = "application/problem+json";
		await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);

		return true;
	}
}
