namespace api_sistema_agente.Domain.Exceptions;

public class UnauthorizedException : Exception
{
  public UnauthorizedException() : base("Unauthorized user")
  {
  }
  public UnauthorizedException(string message) : base(message)
  {
  }
}