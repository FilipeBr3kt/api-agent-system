using System.Text.Json.Serialization;

namespace api_sistema_agente.Domain.ViewModel;

public class ApiResponse<T>
{
  public bool Success { get; set; }
  public T? Data { get; set; }
  [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
  public List<string>? Errors { get; set; }
  public string? Message { get; set; }

  public ApiResponse()
  {
    Errors = Errors?.Count > 0 ? Errors : null;
  }

  public ApiResponse(bool success, T data, List<string> errors, string message)
  {
    Success = success;
    Data = data;
    Message = message;
    Errors = errors?.Count > 0 ? errors : null;
  }
}