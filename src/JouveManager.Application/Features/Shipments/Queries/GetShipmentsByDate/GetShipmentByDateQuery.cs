using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.Shipment;

namespace JouveManager.Application.Features.Shipments.Queries.GetShipmentsByDate;

public class GetShipmentByDateQuery : IQuery<IEnumerable<ShipmentDto>>
{
    public DateTime Date { get; }

    public GetShipmentByDateQuery(DateTime date)
    {
        Date = date;
    }
}