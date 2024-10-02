using api_sistema_agente.Domain.Entities;

namespace api_sistema_agente.Domain.Repositories;

public interface IPendenciesRepository
{
  Task<IEnumerable<Pendencie>> GetAll(string? searchTerm, int take, int skip);
  Task<Pendencie?> GetById(int id);
  Task<Pendencie> Create(Pendencie pendencie);
}