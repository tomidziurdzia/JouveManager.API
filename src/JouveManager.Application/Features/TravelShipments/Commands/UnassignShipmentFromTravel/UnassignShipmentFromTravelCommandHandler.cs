using JouveManager.Application.Contracts.Identity;
using JouveManager.Application.CQRS;
using JouveManager.Application.Exceptions;
using JouveManager.Domain;
using JouveManager.Domain.Enum;
using JouveManager.Domain.Repositories;
using MediatR;

namespace JouveManager.Application.Features.TravelShipments.Commands.UnassignShipmentFromTravel;

public class UnassignShipmentFromTravelCommandHandler(
    ITravelShipmentRepository travelShipmentRepository,
    IShipmentRepository shipmentRepository,
    ITravelShipmentHistoryRepository travelShipmentHistoryRepository,
    IAuthService authService) : ICommandHandler<UnassignShipmentFromTravelCommand, Unit>
{
    public async Task<Unit> Handle(UnassignShipmentFromTravelCommand request, CancellationToken cancellationToken)
    {
        var username = authService.GetSessionUser();

        var shipment = await shipmentRepository.Get(request.ShipmentId, cancellationToken);

        var travelShipment = await travelShipmentRepository.GetTravelShipment(request.ShipmentId, request.TravelId, cancellationToken);
        if (travelShipment == null)
        {
            throw new NotFoundException("TravelShipment", $"ShipmentId: {request.ShipmentId}, TravelId: {request.TravelId}");
        }

        var travelShipmentHistory = new Domain.TravelShipmentHistory
        {
            ShipmentId = shipment.Id,
            OldStatus = travelShipment.ShipmentStatus,
            NewStatus = ShipmentStatus.NotStarted,
            Comments = "Shipment unassigned from travel",
            CreatedAt = DateTime.UtcNow,
            CreatedBy = username,
            LastModified = DateTime.UtcNow,
            LastModifiedBy = username
        };

        await travelShipmentHistoryRepository.Create(travelShipmentHistory, cancellationToken);

        shipment.IsAssigned = false;
        await shipmentRepository.Update(shipment, cancellationToken);

        await travelShipmentRepository.UnassignShipmentFromTravel(request.ShipmentId, request.TravelId, cancellationToken);

        return Unit.Value;
    }
}
