using JouveManager.Domain.Abstractions;

namespace JouveManager.Domain;

public class Shipment : Entity<Guid>
{
    public required string CustomerName { get; set; }
    public required string From { get; set; }
    public required string To { get; set; }
    public required string Description { get; set; }
    public DateTime ScheduledDate { get; set; }
    public bool IsAssigned { get; set; } = false;
    public ICollection<TravelShipment> TravelShipments { get; set; } = new List<TravelShipment>();
}