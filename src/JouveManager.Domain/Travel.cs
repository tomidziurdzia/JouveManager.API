using JouveManager.Domain.Abstractions;

namespace JouveManager.Domain;

public class Travel : Entity<Guid>
{
    public DateTime Date { get; set; }
    public Guid DriverId { get; set; }
    public User Driver { get; set; }
    public Guid? AssistantId { get; set; }
    public User Assistant { get; set; } = null;
    public Guid VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
    public Guid? SemiTrailerId { get; set; }
    public SemiTrailer SemiTrailer { get; set; } = null;
    public ICollection<TravelShipment> TravelShipments { get; set; } = new List<TravelShipment>();
}
