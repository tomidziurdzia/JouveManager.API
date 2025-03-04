using JouveManager.Domain.Abstractions;
using JouveManager.Domain.Enum;

namespace JouveManager.Domain;

public class TravelShipmentHistory : Entity<Guid>
{
    public Guid ShipmentId { get; set; }
    public Shipment Shipment { get; set; }
    public ShipmentStatus OldStatus { get; set; }
    public ShipmentStatus NewStatus { get; set; }
    public string Comments { get; set; }
}