using api_sistema_agente.Application.Services.Interface;
using api_sistema_agente.Domain.Entities;
using api_sistema_agente.Domain.ViewModel;
using api_sistema_agente.Presentation.Controllers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_sistema_agente.Presentation.Controllers;

[ApiController]
[Route("pendencies")]
public class PendenciesController : ControllerBase, IPendenciesController
{
  private readonly IPendenciesService _service;

  public PendenciesController(IPendenciesService service)
  {
    _service = service;
  }

  [HttpGet()]
  [Authorize]
  public async Task<IActionResult> GetPendencies(CancellationToken token, string? searchTerm, int take = 10, int skip = 0)
  {
    return await _service.GetAll(searchTerm, take, skip, token);
  }

  [HttpGet("{id}")]
  [Authorize]
  public async Task<IActionResult> GetPendenciesById(int id, CancellationToken token)
  {
    return await _service.GetById(id, token);
  }

  [HttpPost()]
  [Authorize]
  public async Task<IActionResult> CreatePendencies(PendenciesViewModel model, CancellationToken token)
  {
    var pendencie = new Pendencie(
      model.IdProtocol,
      model.Document,
      model.Name,
      model.IdAgent,
      model.Title,
      model.Description,
      model.ProductName,
      model.AgentName
    );
    return await _service.Create(pendencie, token);
  }

}