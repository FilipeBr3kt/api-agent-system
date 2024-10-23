namespace api_sistema_agente.Domain.Exceptions;

public class UserExistsException : Exception
{
  public UserExistsException() : base("User already exists")
  {
  }
  public UserExistsException(string message) : base(message)
  {
  }
}