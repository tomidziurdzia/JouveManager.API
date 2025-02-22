using FluentValidation;

namespace JouveManager.Application.Features.TravelShipments.Commands.AssignTravelShipment;

public class AssignShipmentToTravelCommandValidator : AbstractValidator<AssignShipmentToTravelCommand>
{
    public AssignShipmentToTravelCommandValidator()
    {
        RuleFor(x => x.ShipmentId).NotEmpty();
        RuleFor(x => x.TravelId).NotEmpty();
    }
}
