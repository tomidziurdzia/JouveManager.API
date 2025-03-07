using JouveManager.Application.CQRS;
using JouveManager.Domain.Repositories;
using MediatR;
using JouveManager.Application.Exceptions;
using FluentValidation.Results;
using JouveManager.Application.Contracts.Identity;
using JouveManager.Domain.Enum;

namespace JouveManager.Application.Features.TravelShipments.Commands.UpdateTravelShipmentStatus;

public class UpdateTravelShipmentStatusCommandHandler(
    ITravelShipmentRepository travelShipmentRepository,
    ITravelShipmentHistoryRepository travelShipmentHistoryRepository,
    IAuthService authService): ICommandHandler<UpdateTravelShipmentStatusCommand, Unit>
{
    public async Task<Unit> Handle(UpdateTravelShipmentStatusCommand request, CancellationToken cancellationToken)
    {
        var username = authService.GetSessionUser();

        var travelShipment = await travelShipmentRepository.GetTravelShipment(
            request.ShipmentId,
            request.TravelId,
            cancellationToken) ?? throw new NotFoundException("TravelShipment", $"ShipmentId: {request.ShipmentId}, TravelId: {request.TravelId}");

        // Validación para estado Delivered
        if (request.ShipmentStatus == ShipmentStatus.Delivered)
        {
            if (!request.DeliveryDate.HasValue)
            {
                throw new ValidationException(new[]
                {
                    new ValidationFailure("DeliveryDate", "Delivery date is required when marking as delivered")
                });
            }
        }

        // Validación para estados Cancelled y Reprogrammed
        if (request.ShipmentStatus == ShipmentStatus.Cancelled || request.ShipmentStatus == ShipmentStatus.Reprogrammed)
        {
            if (string.IsNullOrEmpty(request.FailureReason))
            {
                throw new ValidationException(new[]
                {
                    new ValidationFailure("FailureReason", "Failure reason is required when marking as cancelled or reprogrammed")
                });
            }
        }
        
        var travelShipmentHistory = new Domain.TravelShipmentHistory
        {
            ShipmentId = travelShipment.ShipmentId,
            OldStatus = travelShipment.ShipmentStatus,
            NewStatus = request.ShipmentStatus,
            Comments = request.ShipmentStatus.ToString(),
            CreatedAt = travelShipment.CreatedAt,
            CreatedBy = travelShipment.CreatedBy,
            LastModified = DateTime.UtcNow,
            LastModifiedBy = username
        };

        travelShipment.ShipmentStatus = request.ShipmentStatus;
        travelShipment.DeliveryDate = request.ShipmentStatus == ShipmentStatus.Delivered ? request.DeliveryDate : null;
        travelShipment.FailureReason = (request.ShipmentStatus == ShipmentStatus.Cancelled || request.ShipmentStatus == ShipmentStatus.Reprogrammed)
            ? request.FailureReason
            : null;

        await travelShipmentHistoryRepository.Create(travelShipmentHistory, cancellationToken);

        await travelShipmentRepository.UpdateTravelShipmentAsync(travelShipment, cancellationToken);
        return Unit.Value;
    }
}
