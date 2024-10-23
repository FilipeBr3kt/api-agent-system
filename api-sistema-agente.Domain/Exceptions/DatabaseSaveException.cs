namespace api_sistema_agente.Domain.Exceptions;

public class DatabaseSaveException : Exception
{
  public DatabaseSaveException() : base("An error occurred while saving data to the database")
  {
  }
  public DatabaseSaveException(string message) : base(message)
  {
  }
}