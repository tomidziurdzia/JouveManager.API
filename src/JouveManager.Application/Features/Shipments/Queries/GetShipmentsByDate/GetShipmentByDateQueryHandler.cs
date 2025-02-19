using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.Shipment;
using JouveManager.Domain.Repositories;

namespace JouveManager.Application.Features.Shipments.Queries.GetShipmentsByDate;

public class GetShipmentByDateQueryHandler(IShipmentRepository shipmentRepository) : IQueryHandler<GetShipmentByDateQuery, IEnumerable<ShipmentDto>>
{
    public async Task<IEnumerable<ShipmentDto>> Handle(GetShipmentByDateQuery request, CancellationToken cancellationToken)
    {
        var shipments = await shipmentRepository.GetShipmentsByDate(request.Date, cancellationToken);
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