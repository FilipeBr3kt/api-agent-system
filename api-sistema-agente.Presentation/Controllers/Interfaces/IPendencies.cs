using api_sistema_agente.Domain.ViewModel;

namespace api_sistema_agente.Presentation.Controllers.Interfaces;

public interface IPendenciesController
{

  public Task<IResult> GetPendencies(string? searchTerm, int take = 10, int skip = 0);
  public Task<IResult> GetPendenciesById(int id);
  public Task<IResult> CreatePendencies(PendenciesViewModel model);
}