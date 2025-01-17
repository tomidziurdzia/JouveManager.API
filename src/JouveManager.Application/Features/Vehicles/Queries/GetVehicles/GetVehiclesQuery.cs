using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.Vehicle;

namespace JouveManager.Application.Features.Vehicles.Queries.GetVehicles;

public class GetVehiclesQuery : IQuery<IEnumerable<VehicleDto>>
{

}
