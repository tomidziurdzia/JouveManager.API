using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.TravelShipment;

namespace JouveManager.Application.Features.TravelShipments.Queries.GetTravelsShipments;

public class GetTravelsShipmentsQuery : IQuery<IEnumerable<TravelShipmentsDto>>
{
}