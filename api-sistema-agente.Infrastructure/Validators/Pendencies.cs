using api_sistema_agente.Domain.ViewModel;
using FluentValidation;

namespace api_sistema_agente.Infrastructure.Validators;
public class PendencieValidator : AbstractValidator<PendenciesViewModel>
{
  public PendencieValidator()
  {
    RuleFor(p => p.AgentName).NotEmpty().WithMessage("AgentName is required.");
    RuleFor(p => p.IdProtocol).NotEmpty().WithMessage("IdProtocol is required.");
    RuleFor(p => p.Document).NotEmpty().WithMessage("Document is required.");
    RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required.");
    RuleFor(p => p.Title).NotEmpty().WithMessage("Title is required.");
    RuleFor(p => p.Description).NotEmpty().WithMessage("Description is required.");
    RuleFor(p => p.IdAgent).NotEmpty().WithMessage("IdAgent is required.");
    RuleFor(p => p.ProductName).NotEmpty().WithMessage("ProductName is required.");
  }
}

