using JouveManager.Domain.Enum;

namespace JouveManager.Application.DTOs.TravelShipment;

public class ShipmentTravelsDto
{
    public Guid ShipmentId { get; set; }
    public required string CustomerName { get; set; }
    public required string From { get; set; }
    public required string To { get; set; }
    public required string Description { get; set; }
    public DateTime ScheduledDate { get; set; }
    public ShipmentStatus ShipmentStatus { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public required string FailureReason { get; set; }
}
