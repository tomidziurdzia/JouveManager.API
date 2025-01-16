using JouveManager.Domain.Abstractions;
using JouveManager.Domain.Enum;

namespace JouveManager.Domain;

public class SemiTrailer : Entity<Guid>
{
    public required string LicensePlate { get; set; }
    public required string Brand { get; set; }
    public required string Model { get; set; }
    public required string ImageUrl { get; set; }
    public TypeSemiTrailer Type { get; set; }
    public virtual ICollection<Travel> Travels { get; set; } = new List<Travel>();
}
