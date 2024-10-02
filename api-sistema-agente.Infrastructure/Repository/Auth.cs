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

  public async Task<Auth> CreateUser(Auth user)
  {
    _context.Auth.Add(user);
    await _context.SaveChangesAsync();
    return user;
  }

  public async Task<Auth?> FindUserByMail(string mail)
  {
    Auth? user = await _context.Auth.FirstOrDefaultAsync(p => p.Mail == mail);

    if (user != null)
    {
      return user;
    }

    return null;
  }

  public async Task<Auth?> FindUserByName(string name)
  {
    Auth? user = await _context.Auth.FirstOrDefaultAsync(p => p.Login == name);

    if (user != null)
    {
      return user;
    }

    return null;
  }
}