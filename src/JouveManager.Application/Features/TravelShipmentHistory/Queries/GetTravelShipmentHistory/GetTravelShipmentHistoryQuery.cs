using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.TravelShipmentHistory;

namespace JouveManager.Application.Features.TravelShipmentHistory.Queries.GetTravelShipmentHistory;

public class GetTravelShipmentHistoryQuery : IQuery<IEnumerable<TravelShipmentHistoryDto>>
{
    public Guid ShipmentId { get; set; }
    public GetTravelShipmentHistoryQuery(Guid shipmentId)
    {
        ShipmentId = shipmentId;
    }
}
