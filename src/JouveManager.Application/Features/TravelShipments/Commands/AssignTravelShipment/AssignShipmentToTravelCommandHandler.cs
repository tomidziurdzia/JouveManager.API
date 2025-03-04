using System.ComponentModel.DataAnnotations;
using JouveManager.Application.Contracts.Identity;
using JouveManager.Application.CQRS;
using JouveManager.Domain;
using JouveManager.Domain.Enum;
using JouveManager.Domain.Repositories;
using MediatR;

namespace JouveManager.Application.Features.TravelShipments.Commands.AssignTravelShipment;

public class AssignShipmentToTravelCommandHandler(ITravelShipmentRepository travelShipmentRepository, IShipmentRepository shipmentRepository, ITravelShipmentHistoryRepository travelShipmentHistoryRepository, IAuthService authService) : ICommandHandler<AssignShipmentToTravelCommand, Unit>
{
    public async Task<Unit> Handle(AssignShipmentToTravelCommand request, CancellationToken cancellationToken)
    {
        var username = authService.GetSessionUser();

        var activeAssignments = await travelShipmentRepository.GetActiveAssignmentsByShipmentId(request.ShipmentId, cancellationToken);

        if (activeAssignments.Any(ts => ts.ShipmentStatus == ShipmentStatus.Delivered && string.IsNullOrEmpty(ts.FailureReason)))
        {
            throw new ValidationException($"Cannot assign Shipment {request.ShipmentId} to a new Travel because it is already assigned to another active Travel.");
        }

        var shipment = await shipmentRepository.Get(request.ShipmentId, cancellationToken);
        shipment.IsAssigned = true;

        await shipmentRepository.Update(shipment, cancellationToken);

        Console.WriteLine(shipment);

        var travelShipment = await travelShipmentRepository.AssignShipmentToTravel(request.ShipmentId, request.TravelId, cancellationToken);

        var travelShipmentHistory = new TravelShipmentHistory
        {
            TravelShipmentId = travelShipment.Id,
            OldStatus = ShipmentStatus.NotStarted,
            NewStatus = ShipmentStatus.InProgress,
            Comments = $"Shipment assigned to travel",
            CreatedAt = shipment.CreatedAt,
            CreatedBy = shipment.CreatedBy,
            LastModified = DateTime.UtcNow,
            LastModifiedBy = username
        };

        await travelShipmentHistoryRepository.Create(travelShipmentHistory, cancellationToken);

        return Unit.Value;
    }
}
