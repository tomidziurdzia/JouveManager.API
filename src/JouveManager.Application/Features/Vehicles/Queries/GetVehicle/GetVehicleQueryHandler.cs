using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.Vehicle;
using JouveManager.Application.Exceptions;
using JouveManager.Domain;
using JouveManager.Domain.Repositories;

namespace JouveManager.Application.Features.Vehicles.Queries.GetVehicle;

public class GetVehicleQueryHandler(IVehicleRepository vehicleRepository) : IQueryHandler<GetVehicleQuery, VehicleDto>
{
    public async Task<VehicleDto> Handle(GetVehicleQuery request, CancellationToken cancellationToken)
    {
        var vehicle = await vehicleRepository.Get(request.Id, cancellationToken);
        if (vehicle == null) throw new NotFoundException(nameof(Vehicle), request.Id);

        return new VehicleDto
        {
            Id = vehicle.Id,
            LicensePlate = vehicle.LicensePlate,
            Brand = vehicle.Brand,
            Model = vehicle.Model,
            ImageUrl = vehicle.ImageUrl,
            Type = vehicle.Type
        };
    }
}
