using api_sistema_agente.Domain.Entities;
using api_sistema_agente.Domain.Repositories;
using api_sistema_agente.Infrastructure.Config;
using Microsoft.EntityFrameworkCore;

namespace api_sistema_agente.Infrastructure.Repository;

public class PendenciesRepository : IPendenciesRepository
{
  private readonly DatabaseContext _context;

  public PendenciesRepository(DatabaseContext context)
  {
    _context = context;
  }
  public async Task<Pendencie> Create(Pendencie pendencie)
  {
    await _context.Pendencias.AddAsync(pendencie);
    await _context.SaveChangesAsync();
    return pendencie;
  }

  public async Task<IEnumerable<Pendencie>> GetAll(string? searchTerm, int take, int skip)
  {
    if (!string.IsNullOrWhiteSpace(searchTerm))
    {
      return await _context.Pendencias.Where(
          p => p.Title.Contains(searchTerm) ||
          p.Name.Contains(searchTerm) ||
          p.Document.Contains(searchTerm)
          )
          .Take(take)
          .Skip(skip)
          .ToListAsync();
    }

    return await _context.Pendencias
      .Take(take)
      .Skip(skip)
      .ToListAsync();
  }

  public async Task<Pendencie?> GetById(int id)
  {
    return await _context.Pendencias.FindAsync(id);
  }
}