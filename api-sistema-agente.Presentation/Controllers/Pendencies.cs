using api_sistema_agente.Application.Services.Interface;
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
  public async Task<IResult> GetPendencies(string? searchTerm, int take = 10, int skip = 0)
  {

    return await _service.GetAll(searchTerm, take, skip);
  }

  [HttpGet("{id}")]
  [Authorize]
  public async Task<IResult> GetPendenciesById(int id)
  {
    return await _service.GetById(id);
  }

  [HttpPost()]
  [Authorize]
  public async Task<IResult> CreatePendencies(PendenciesViewModel model)
  {
    return await _service.Create(model);
  }

}