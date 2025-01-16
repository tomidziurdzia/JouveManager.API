using JouveManager.Domain.Abstractions;
using JouveManager.Domain.Enum;

namespace JouveManager.Domain;

public class SemiTrailer : Entity<Guid>
{
    public string LicensePlate { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string ImageUrl { get; set; }
    public TypeSemiTrailer Type { get; set; }
    public virtual ICollection<Travel> Travels { get; set; } = new List<Travel>();
}
