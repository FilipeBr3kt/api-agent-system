namespace api_sistema_agente.Domain.Exceptions;

public class ValidationException : Exception
{
  public List<string> Errors { get; }
  public ValidationException(List<string> errors) : base("Validation failed with one or more errors.")
  {
    Errors = errors;
  }

  public ValidationException(string message, List<string> errors) : base(message)
  {
    Errors = errors;
  }
}