using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.Shipment;
using JouveManager.Domain.Repositories;

namespace JouveManager.Application.Features.Shipments.Queries.GetShipments;

public class GetShipmentsQueryHandler(IShipmentRepository shipmentRepository) : IQueryHandler<GetShipmentsQuery, IEnumerable<ShipmentDto>>
{
    public async Task<IEnumerable<ShipmentDto>> Handle(GetShipmentsQuery request, CancellationToken cancellationToken)
    {
        var shipments = await shipmentRepository.GetAll(cancellationToken);
        return shipments.Select(s => new ShipmentDto
        {
            Id = s.Id,
            CustomerName = s.CustomerName,
            From = s.From,
            To = s.To,
            Description = s.Description,
            ScheduledDate = s.ScheduledDate,
        });
    }
}
