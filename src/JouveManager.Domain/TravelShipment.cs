using JouveManager.Domain;
using JouveManager.Domain.Abstractions;

public class TravelShipment : Entity<Guid>
{
    public Guid ShipmentId { get; set; }
    public Shipment Shipment { get; set; }

    public Guid TravelId { get; set; }
    public Travel Travel { get; set; }

    public bool Delivered { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public string FailureReason { get; set; } = string.Empty;
}