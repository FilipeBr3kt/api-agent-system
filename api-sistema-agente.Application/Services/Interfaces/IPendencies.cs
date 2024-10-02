using api_sistema_agente.Domain.Entities;
using api_sistema_agente.Domain.ViewModel;
using Microsoft.AspNetCore.Http;

namespace api_sistema_agente.Application.Services.Interface;

public interface IPendenciesService
{
  Task<IResult> GetAll(string? searchTerm, int take, int skip);
  Task<IResult> GetById(int id);
  Task<IResult> Create(PendenciesViewModel model);
  // Task<IResult> Update(Pendencie pendencie);
  // Task<IResult> Delete(int id);
}