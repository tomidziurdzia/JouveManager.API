using System.ComponentModel.DataAnnotations;
using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.TravelShipment;
using JouveManager.Domain.Repositories;
using MediatR;

namespace JouveManager.Application.Features.TravelShipments.Commands.AssignTravelShipment;

public class AssignShipmentToTravelCommandHandler(ITravelShipmentRepository travelShipmentRepository) : ICommandHandler<AssignShipmentToTravelCommand, Unit>
{
    public async Task<Unit> Handle(AssignShipmentToTravelCommand request, CancellationToken cancellationToken)
    {
        var activeAssignments = await travelShipmentRepository.GetActiveAssignmentsByShipmentId(request.ShipmentId, cancellationToken);

        if (activeAssignments.Any(ts => !ts.Delivered && string.IsNullOrEmpty(ts.FailureReason)))
        {
            throw new ValidationException($"Cannot assign Shipment {request.ShipmentId} to a new Travel because it is already assigned to another active Travel.");
        }

        await travelShipmentRepository.AssignShipmentToTravel(request.ShipmentId, request.TravelId, cancellationToken);

        return Unit.Value;
    }
}
