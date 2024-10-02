using api_sistema_agente.Domain.Entities;

namespace api_sistema_agente.Domain.Repositories;

public interface IPendenciesRepository
{
  Task<IEnumerable<Pendencie>> GetAll(string? searchTerm, int take, int skip, CancellationToken token);
  Task<Pendencie?> GetById(int id, CancellationToken token);
  Task<Pendencie> Create(Pendencie pendencie, CancellationToken token);
}