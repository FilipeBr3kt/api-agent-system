using System.Data;
using api_sistema_agente.Application.Services.Interface;
using api_sistema_agente.Domain.Entities;
using api_sistema_agente.Domain.Repositories;
using api_sistema_agente.Domain.ViewModel;
using api_sistema_agente.Infrastructure.Validators;
using Microsoft.AspNetCore.Http;

namespace api_sistema_agente.Application.Services;

public class PendenciesService : IPendenciesService
{
  private readonly IPendenciesRepository _repository;
  private readonly PendencieValidator _validator;

  public PendenciesService(IPendenciesRepository repository)
  {
    _repository = repository;
    _validator = new PendencieValidator();
  }

  public async Task<IResult> Create(PendenciesViewModel model)
  {
    var validationResult = _validator.Validate(model);

    if (!validationResult.IsValid)
    {
      var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
      return Results.BadRequest(errors);
    }

    Pendencie pendencie = new Pendencie
    {
      Title = model.Title!,
      Description = model.Description!,
      AgentName = model.AgentName,
      IdProtocol = model.IdProtocol!,
      IdAgent = model.IdAgent,
      ProductName = model.ProductName!,
      Document = model.Document!,
      Name = model.Name,
      DateRegister = BitConverter.GetBytes(DateTime.UtcNow.Ticks),
    };

    await _repository.Create(pendencie);
    return Results.Created("", pendencie);
  }

  public async Task<IResult> GetAll(string? searchTerm, int take, int skip)
  {

    IEnumerable<Pendencie> pendencies = await _repository.GetAll(searchTerm, take, skip);

    if (!pendencies.Any()) return Results.NotFound();

    return Results.Ok(pendencies);
  }

  public async Task<IResult> GetById(int id)
  {
    Pendencie? pendencie = await _repository.GetById(id);

    if (pendencie is null) return Results.NotFound();

    return Results.Ok(pendencie);
  }
}