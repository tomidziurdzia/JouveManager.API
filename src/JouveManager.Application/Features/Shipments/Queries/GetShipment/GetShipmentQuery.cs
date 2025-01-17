using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.Shipment;

namespace JouveManager.Application.Features.Shipments.Queries.GetShipment;

public class GetShipmentQuery : IQuery<ShipmentDto>
{
    public Guid Id { get; set; }
    public GetShipmentQuery(Guid id)
    {
        Id = id;
    }
}
