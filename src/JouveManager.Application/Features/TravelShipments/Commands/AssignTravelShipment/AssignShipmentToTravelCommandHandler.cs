using System.ComponentModel.DataAnnotations;
using JouveManager.Application.CQRS;
using JouveManager.Domain.Enum;
using JouveManager.Domain.Repositories;
using MediatR;

namespace JouveManager.Application.Features.TravelShipments.Commands.AssignTravelShipment;

public class AssignShipmentToTravelCommandHandler(ITravelShipmentRepository travelShipmentRepository, IShipmentRepository shipmentRepository) : ICommandHandler<AssignShipmentToTravelCommand, Unit>
{
    public async Task<Unit> Handle(AssignShipmentToTravelCommand request, CancellationToken cancellationToken)
    {
        var activeAssignments = await travelShipmentRepository.GetActiveAssignmentsByShipmentId(request.ShipmentId, cancellationToken);

        if (activeAssignments.Any(ts => ts.ShipmentStatus == ShipmentStatus.Delivered && string.IsNullOrEmpty(ts.FailureReason)))
        {
            throw new ValidationException($"Cannot assign Shipment {request.ShipmentId} to a new Travel because it is already assigned to another active Travel.");
        }

        var shipment = await shipmentRepository.Get(request.ShipmentId, cancellationToken);
        shipment.IsAssigned = true;
        shipment.TravelShipments.Add(new TravelShipment
        {
            TravelId = request.TravelId,
            ShipmentId = request.ShipmentId,
            ShipmentStatus = ShipmentStatus.InProgress,
            CreatedAt = DateTime.UtcNow,
        });
        await shipmentRepository.Update(shipment, cancellationToken);

        Console.WriteLine(shipment);

        await travelShipmentRepository.AssignShipmentToTravel(request.ShipmentId, request.TravelId, cancellationToken);

        return Unit.Value;
    }
}
