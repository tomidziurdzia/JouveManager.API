using JouveManager.Application.CQRS;
using JouveManager.Application.Exceptions;
using JouveManager.Domain.Repositories;
using MediatR;

namespace JouveManager.Application.Features.TravelShipments.Commands.UnassignShipmentFromTravel;

public class UnassignShipmentFromTravelCommandHandler(ITravelShipmentRepository travelShipmentRepository) : ICommandHandler<UnassignShipmentFromTravelCommand, Unit>
{
    public async Task<Unit> Handle(UnassignShipmentFromTravelCommand request, CancellationToken cancellationToken)
    {
        var travelShipment = await travelShipmentRepository.GetTravelShipment(request.ShipmentId, request.TravelId, cancellationToken);
        if (travelShipment == null)
        {
            throw new NotFoundException("TravelShipment", $"ShipmentId: {request.ShipmentId}, TravelId: {request.TravelId}");
        }

        await travelShipmentRepository.UnassignShipmentFromTravel(request.ShipmentId, request.TravelId, cancellationToken);

        return Unit.Value;
    }
}
