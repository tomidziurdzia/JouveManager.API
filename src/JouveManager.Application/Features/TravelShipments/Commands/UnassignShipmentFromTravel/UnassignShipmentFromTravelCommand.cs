using JouveManager.Application.CQRS;
using MediatR;

namespace JouveManager.Application.Features.TravelShipments.Commands.UnassignShipmentFromTravel;

public class UnassignShipmentFromTravelCommand : ICommand<Unit>
{
    public required Guid ShipmentId { get; set; }
    public required Guid TravelId { get; set; }
}
