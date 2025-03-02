using JouveManager.Domain.Abstractions;
using JouveManager.Domain.Enum;

namespace JouveManager.Domain;

public class TravelShipment : Entity<Guid>
{
    public Guid ShipmentId { get; set; }
    public Shipment Shipment { get; set; }
    public Guid TravelId { get; set; }
    public Travel Travel { get; set; }
    public ShipmentStatus ShipmentStatus { get; set; } = ShipmentStatus.NotStarted;
    public DateTime? DeliveryDate { get; set; }
    public string FailureReason { get; set; } = null;
    public ICollection<TravelShipmentHistory> TravelShipmentHistory { get; set; } = new List<TravelShipmentHistory>();
}