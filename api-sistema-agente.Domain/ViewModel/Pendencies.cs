namespace api_sistema_agente.Domain.ViewModel;

public record PendenciesViewModel(
  string IdProtocol,
  string Document,
  string Name,
  int IdAgent,
  string Title,
  string Description,
  string ProductName,
  string AgentName
  );