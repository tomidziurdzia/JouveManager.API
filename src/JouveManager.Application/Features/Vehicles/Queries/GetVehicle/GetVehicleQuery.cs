using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.Vehicle;

namespace JouveManager.Application.Features.Vehicles.Queries.GetVehicle;

public class GetVehicleQuery : IQuery<VehicleDto>
{
    public Guid Id { get; set; }
    public GetVehicleQuery(Guid id)
    {
        Id = id;
    }
}
