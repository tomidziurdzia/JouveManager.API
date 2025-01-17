using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.Vehicle;
using JouveManager.Domain.Repositories;

namespace JouveManager.Application.Features.Vehicles.Queries.GetVehicles;

public class GetVehiclesQueryHandler(IVehicleRepository vehicleRepository) : IQueryHandler<GetVehiclesQuery, IEnumerable<VehicleDto>>
{
    public async Task<IEnumerable<VehicleDto>> Handle(GetVehiclesQuery request, CancellationToken cancellationToken)
    {
        var vehicles = await vehicleRepository.GetAll(cancellationToken);

        return vehicles.Select(
            v => new VehicleDto
            {
                Id = v.Id,
                LicensePlate = v.LicensePlate,
                Brand = v.Brand,
                Model = v.Model,
                ImageUrl = v.ImageUrl,
                Type = v.Type
            });
    }
}
