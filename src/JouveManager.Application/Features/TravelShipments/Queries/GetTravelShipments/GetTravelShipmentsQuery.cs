using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.TravelShipment;

namespace JouveManager.Application.Features.TravelShipments.Queries.GetTravelShipments;

public class GetTravelShipmentsQuery : IQuery<TravelShipmentsDto>
{
    public Guid TravelId { get; set; }
    public GetTravelShipmentsQuery(Guid travelId)
    {
        TravelId = travelId;
    }
}
