using FluentValidation;

namespace JouveManager.Application.Features.TravelShipments.Commands.UnassignShipmentFromTravel;

public class UnassignShipmentFromTravelCommandValidator : AbstractValidator<UnassignShipmentFromTravelCommand>
{
    public UnassignShipmentFromTravelCommandValidator()
    {
        RuleFor(x => x.ShipmentId).NotEmpty();
        RuleFor(x => x.TravelId).NotEmpty();
    }
}
