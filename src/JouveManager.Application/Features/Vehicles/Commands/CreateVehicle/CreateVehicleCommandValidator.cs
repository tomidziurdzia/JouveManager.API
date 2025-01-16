using FluentValidation;
using JouveManager.Domain.Enum;

namespace JouveManager.Application.Features.Vehicles.Commands.CreateVehicle;

public class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommand>
{
    public CreateVehicleCommandValidator()
    {
        RuleFor(x => x.LicensePlate)
            .NotEmpty().WithMessage("License plate is required");
        RuleFor(x => x.Brand)
            .NotEmpty().WithMessage("Brand is required");
        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("Model is required");
        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Type is required");
    }
}
