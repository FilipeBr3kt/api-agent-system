namespace api_sistema_agente.Domain.Exceptions;

public class InvalidPasswordException : Exception
{
  public InvalidPasswordException() : base("Invalid password")
  {
  }
  public InvalidPasswordException(string message) : base(message)
  {
  }
}