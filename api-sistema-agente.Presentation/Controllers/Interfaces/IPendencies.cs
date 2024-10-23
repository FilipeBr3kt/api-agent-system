using api_sistema_agente.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace api_sistema_agente.Presentation.Controllers.Interfaces;

public interface IPendenciesController
{

  public Task<IActionResult> GetPendencies(CancellationToken token, string? searchTerm, int take = 10, int skip = 0);
  public Task<IActionResult> GetPendenciesById(int id, CancellationToken token);
  public Task<IActionResult> CreatePendencies(PendenciesViewModel model, CancellationToken token);
}