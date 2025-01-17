using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.Shipment;
using JouveManager.Application.Exceptions;
using JouveManager.Domain;
using JouveManager.Domain.Repositories;

namespace JouveManager.Application.Features.Shipments.Queries.GetShipment;

public class GetShipmentQueryHandler(IShipmentRepository shipmentRepository) : IQueryHandler<GetShipmentQuery, ShipmentDto>
{
    public async Task<ShipmentDto> Handle(GetShipmentQuery request, CancellationToken cancellationToken)
    {
        var shipment = await shipmentRepository.Get(request.Id, cancellationToken);
        if (shipment is null) throw new NotFoundException(nameof(Shipment), request.Id);

        return new ShipmentDto
        {
            Id = shipment.Id,
            CustomerName = shipment.CustomerName,
            From = shipment.From,
            To = shipment.To,
            Description = shipment.Description,
            ScheduledDate = shipment.ScheduledDate,
        };
    }
}
