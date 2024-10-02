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
  public async Task<Pendencie> Create(Pendencie pendencie, CancellationToken token)
  {
    await _context.Pendencias.AddAsync(pendencie, token);
    await _context.SaveChangesAsync(token);
    return pendencie;
  }

  public async Task<IEnumerable<Pendencie>> GetAll(string? searchTerm, int take, int skip, CancellationToken token)
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
          .ToListAsync(token);
    }

    return await _context.Pendencias
      .Take(take)
      .Skip(skip)
      .ToListAsync(token);
  }

  public async Task<Pendencie?> GetById(int id, CancellationToken token)
  {
    return await _context.Pendencias.FindAsync(id);
  }
}