using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.Vehicle;
using JouveManager.Domain;
using JouveManager.Domain.Repositories;

namespace JouveManager.Application.Features.Vehicles.Commands.CreateVehicle;

public class CreateVehicleCommandHandler(IVehicleRepository vehicleRepository) : ICommandHandler<CreateVehicleCommand, VehicleDto>
{
    public async Task<VehicleDto> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
    {
        var vehicle = new Vehicle
        {
            LicensePlate = request.LicensePlate,
            Brand = request.Brand,
            Model = request.Model,
            ImageUrl = request.ImageUrl,
            Type = request.Type,
        };

        await vehicleRepository.Create(vehicle, cancellationToken);

        return new VehicleDto
        {
            Id = vehicle.Id,
            LicensePlate = vehicle.LicensePlate,
            Brand = vehicle.Brand,
            Model = vehicle.Model,
            ImageUrl = vehicle.ImageUrl,
            Type = vehicle.Type,
        };
    }
}
