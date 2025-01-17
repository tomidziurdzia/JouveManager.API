using FluentValidation;

namespace JouveManager.Application.Features.Shipments.Command.UpdateShipment;

public class UpdateShipmentCommandValidator : AbstractValidator<UpdateShipmentCommand>
{
    public UpdateShipmentCommandValidator()
    {
        RuleFor(x => x.CustomerName)
            .NotEmpty().WithMessage("Customer name is required");
        RuleFor(x => x.From)
            .NotEmpty().WithMessage("From is required");
        RuleFor(x => x.To)
            .NotEmpty().WithMessage("To is required");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required");
    }
}
