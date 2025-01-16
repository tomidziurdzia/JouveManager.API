using System.Text.Json.Serialization;
using JouveManager.Domain.Enum;

namespace JouveManager.Application.DTOs.Vehicle;

public class CreateVehicleDto
{
    public string? LicensePlate { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public string? ImageUrl { get; set; }
    public TypeVehicle? Type { get; set; }
}
