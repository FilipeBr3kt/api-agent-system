using api_sistema_agente.Domain.Entities;
using api_sistema_agente.Domain.Repositories;
using api_sistema_agente.Infrastructure.Config;
using Microsoft.EntityFrameworkCore;

namespace api_sistema_agente.Infrastructure.Repository;

public class AuthRepository : IAuthRepository
{
  private readonly DatabaseContext _context;

  public AuthRepository(DatabaseContext context)
  {
    _context = context;
  }

  public async Task<Auth> SaveUser(Auth user, CancellationToken token)
  {
    await _context.Auth.AddAsync(user, token);
    await _context.SaveChangesAsync(token);
    return user;
  }

  public async Task<Auth?> FindUserByMail(string mail, CancellationToken token)
  {
    Auth? user = await _context.Auth.FirstOrDefaultAsync(p => p.Mail == mail, token);

    if (user != null)
    {
      return user;
    }

    return null;
  }

  public async Task<Auth?> FindUserByName(string name, CancellationToken token)
  {
    Auth? user = await _context.Auth.FirstOrDefaultAsync(p => p.Login == name, token);

    if (user != null)
    {
      return user;
    }

    return null;
  }
}