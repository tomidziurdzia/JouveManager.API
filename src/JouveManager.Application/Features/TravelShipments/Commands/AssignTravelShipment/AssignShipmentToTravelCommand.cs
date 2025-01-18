using JouveManager.Application.CQRS;
using MediatR;

namespace JouveManager.Application.Features.TravelShipments.Commands.AssignTravelShipment;

public class AssignShipmentToTravelCommand : ICommand<Unit>
{
    public required Guid ShipmentId { get; set; }
    public required Guid TravelId { get; set; }
}
