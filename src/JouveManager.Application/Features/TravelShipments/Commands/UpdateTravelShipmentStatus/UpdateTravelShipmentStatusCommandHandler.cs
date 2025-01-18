using JouveManager.Application.CQRS;
using JouveManager.Domain.Repositories;
using MediatR;
using JouveManager.Application.Exceptions;
using FluentValidation.Results;

namespace JouveManager.Application.Features.TravelShipments.Commands.UpdateTravelShipmentStatus;

public class UpdateTravelShipmentStatusCommandHandler(ITravelShipmentRepository travelShipmentRepository) : ICommandHandler<UpdateTravelShipmentStatusCommand, Unit>
{
    public async Task<Unit> Handle(UpdateTravelShipmentStatusCommand request, CancellationToken cancellationToken)
    {
        var travelShipment = await travelShipmentRepository.GetTravelShipment(
            request.ShipmentId,
            request.TravelId,
            cancellationToken) ?? throw new NotFoundException("TravelShipment", $"ShipmentId: {request.ShipmentId}, TravelId: {request.TravelId}");

        if (request.Delivered && !request.DeliveryDate.HasValue)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure("DeliveryDate", "Delivery date is required when marking as delivered")
            });
        }

        if (!request.Delivered && string.IsNullOrEmpty(request.FailureReason))
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure("FailureReason", "Failure reason is required when marking as not delivered")
            });
        }

        travelShipment.Delivered = request.Delivered;
        travelShipment.DeliveryDate = request.Delivered ? request.DeliveryDate : null;
        travelShipment.FailureReason = request.Delivered ? null : request.FailureReason;

        await travelShipmentRepository.UpdateTravelShipmentAsync(travelShipment, cancellationToken);
        return Unit.Value;
    }
}
