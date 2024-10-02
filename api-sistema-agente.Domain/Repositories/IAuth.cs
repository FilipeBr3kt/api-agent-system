using api_sistema_agente.Domain.Entities;
using api_sistema_agente.Domain.ViewModel;

namespace api_sistema_agente.Domain.Repositories;

public interface IAuthRepository
{
  public Task<Auth?> FindUserByName(string name);
  public Task<Auth> CreateUser(Auth user);
  public Task<Auth?> FindUserByMail(string mail);
}