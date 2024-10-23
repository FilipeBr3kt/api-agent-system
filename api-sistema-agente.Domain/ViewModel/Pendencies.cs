namespace api_sistema_agente.Domain.ViewModel;

public class PendenciesViewModel
{
  public string IdProtocol { get; set; } = null!;
  public string Document { get; set; } = null!;
  public string Name { get; set; } = null!;
  public int IdAgent { get; set; }
  public string Title { get; set; } = null!;
  public string Description { get; set; } = null!;
  public string ProductName { get; set; } = null!;
  public string AgentName { get; set; } = null!;
}
