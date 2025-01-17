using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.Shipment;
using JouveManager.Domain;
using JouveManager.Domain.Repositories;

namespace JouveManager.Application.Features.Shipments.Command.CreateShipment;

public class CreateShipmentCommandHandler(IShipmentRepository shipmentRepository) : ICommandHandler<CreateShipmentCommand, ShipmentDto>
{
    public async Task<ShipmentDto> Handle(CreateShipmentCommand request, CancellationToken cancellationToken)
    {
        var shipment = new Shipment
        {
            CustomerName = request.CustomerName,
            From = request.From,
            To = request.To,
            Description = request.Description,
            ScheduledDate = request.ScheduledDate,
        };

        await shipmentRepository.Create(shipment, cancellationToken);

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
