using api_sistema_agente.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace api_sistema_agente.Application.Services.Interface;

public interface IPendenciesService
{
  Task<IActionResult> GetAll(string? searchTerm, int take, int skip, CancellationToken token);
  Task<IActionResult> GetById(int id, CancellationToken token);
  Task<IActionResult> Create(Pendencie pendencie, CancellationToken token);
  // Task<IResult> Update(Pendencie pendencie);
  // Task<IResult> Delete(int id);
}