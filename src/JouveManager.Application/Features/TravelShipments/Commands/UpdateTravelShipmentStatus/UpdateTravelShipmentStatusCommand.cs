using System;
using JouveManager.Application.CQRS;
using MediatR;

namespace JouveManager.Application.Features.TravelShipments.Commands.UpdateTravelShipmentStatus;

public class UpdateTravelShipmentStatusCommand : ICommand<Unit>
{
    public required Guid ShipmentId { get; set; }
    public required Guid TravelId { get; set; }
    public bool Delivered { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public string? FailureReason { get; set; }
}
