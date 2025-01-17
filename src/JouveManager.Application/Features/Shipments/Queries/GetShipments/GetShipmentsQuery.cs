using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.Shipment;

namespace JouveManager.Application.Features.Shipments.Queries.GetShipments;

public class GetShipmentsQuery : IQuery<IEnumerable<ShipmentDto>>
{

}
