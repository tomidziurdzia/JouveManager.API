using JouveManager.Domain.Abstractions;
using JouveManager.Domain.Enum;

namespace JouveManager.Domain;

public class Vehicle : Entity<Guid>
{
    public required string LicensePlate { get; set; }
    public required string Brand { get; set; }
    public required string Model { get; set; }
    public required string ImageUrl { get; set; }
    public TypeVehicle Type { get; set; }
    public bool RequiresTrailer { get; set; }
    public virtual ICollection<Travel> Travels { get; set; } = new List<Travel>();
}