using api_sistema_agente.Domain.Entities;
using api_sistema_agente.Domain.ViewModel;

namespace api_sistema_agente.Domain.Repositories;

public interface IAuthRepository
{
  public Task<Auth?> FindUserByName(string name, CancellationToken token);
  public Task<Auth> SaveUser(Auth user, CancellationToken token);
  public Task<Auth?> FindUserByMail(string mail, CancellationToken token);
}