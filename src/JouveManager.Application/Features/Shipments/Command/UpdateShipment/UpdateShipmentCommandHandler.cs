using JouveManager.Application.CQRS;
using JouveManager.Application.Exceptions;
using JouveManager.Domain;
using JouveManager.Domain.Repositories;
using MediatR;

namespace JouveManager.Application.Features.Shipments.Command.UpdateShipment;

public class UpdateShipmentCommandHandler(IShipmentRepository shipmentRepository) : ICommandHandler<UpdateShipmentCommand, Unit>
{
    public async Task<Unit> Handle(UpdateShipmentCommand request, CancellationToken cancellationToken)
    {
        var shipment = await shipmentRepository.Get(request.Id, cancellationToken);
        if (shipment is null) throw new NotFoundException(nameof(Shipment), request.Id);

        shipment.CustomerName = request.CustomerName ?? shipment.CustomerName;
        shipment.From = request.From ?? shipment.From;
        shipment.To = request.To ?? shipment.To;
        shipment.Description = request.Description ?? shipment.Description;
        shipment.ScheduledDate = request.ScheduledDate;

        await shipmentRepository.Update(shipment, cancellationToken);
        return Unit.Value;
    }
}
