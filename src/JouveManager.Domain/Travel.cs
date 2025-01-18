using JouveManager.Domain.Abstractions;

namespace JouveManager.Domain;

public class Travel : Entity<Guid>
{
    public DateTime Date { get; set; }
    public required string DriverId { get; set; }
    public User Driver { get; set; }
    public string AssistantId { get; set; }
    public User Assistant { get; set; }
    public required Guid VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
    public Guid? SemiTrailerId { get; set; }
    public SemiTrailer SemiTrailer { get; set; }
    public ICollection<TravelShipment> TravelShipments { get; set; } = new List<TravelShipment>();
}
