using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.Vehicle;
using JouveManager.Domain.Enum;

namespace JouveManager.Application.Features.Vehicles.Commands.CreateVehicle;

public class CreateVehicleCommand : ICommand<VehicleDto>
{
    public required string LicensePlate { get; set; }
    public required string Brand { get; set; }
    public required string Model { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public required TypeVehicle Type { get; set; }
}
