using System;
using JouveManager.Domain.Abstractions;
using JouveManager.Domain.Enum;

namespace JouveManager.Domain;

public class Vehicle : Entity<Guid>
{
    public string LicensePlate { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string ImageUrl { get; set; }
    public TypeVehicle Type { get; set; }
    public bool RequiresTrailer { get; set; }
    public virtual ICollection<Travel> Travels { get; set; } = new List<Travel>();
}