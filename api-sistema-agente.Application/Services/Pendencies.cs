using System.Data;
using api_sistema_agente.Application.Services.Interface;
using api_sistema_agente.Domain.Entities;
using api_sistema_agente.Domain.Exceptions;
using api_sistema_agente.Domain.Repositories;
using api_sistema_agente.Domain.ViewModel;
using api_sistema_agente.Infrastructure.Validators;
using Microsoft.AspNetCore.Mvc;

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

  public async Task<IActionResult> Create(Pendencie pendencie, CancellationToken token)
  {
    var validationResult = _validator.Validate(pendencie);

    if (!validationResult.IsValid)
    {
      var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
      throw new ValidationException(errors);
    }

    var assertPendencie = await _repository.Save(pendencie, token);
    if (assertPendencie == null)
    {
      throw new DatabaseSaveException();
    }

    return new OkObjectResult(new ApiResponse<object>
    {
      Success = true,
      Data = new { Message = "Pendencie was created successfully" }
    });
  }

  public async Task<IActionResult> GetAll(string? searchTerm, int take, int skip, CancellationToken token)
  {

    IEnumerable<Pendencie> pendencies = await _repository.GetAll(searchTerm, take, skip, token);

    if (!pendencies.Any())
    {
      throw new NotFoundException("No pendencies found");
    }

    return new OkObjectResult(new ApiResponse<IEnumerable<Pendencie>>
    {
      Success = true,
      Data = pendencies
    });
  }

  public async Task<IActionResult> GetById(int id, CancellationToken token)
  {
    Pendencie? pendencie = await _repository.GetById(id, token);

    if (pendencie is null)
    {
      throw new NotFoundException("Pendencie not found");
    }

    return new OkObjectResult(new ApiResponse<Pendencie>
    {
      Success = true,
      Data = pendencie
    });
  }
}