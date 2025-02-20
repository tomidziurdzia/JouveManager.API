using JouveManager.Domain.Enum;

namespace JouveManager.Application.DTOs.Vehicle;

public class UpdateVehicleDto
{
    public string? LicensePlate { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public string? ImageUrl { get; set; }
    public TypeVehicle? Type { get; set; }
}
