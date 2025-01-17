using JouveManager.Application.CQRS;
using JouveManager.Application.Exceptions;
using JouveManager.Domain;
using JouveManager.Domain.Repositories;
using MediatR;

namespace JouveManager.Application.Features.Shipments.Command.DeleteShipment;

public class DeleteShipmentCommandHandler(IShipmentRepository shipmentRepository) : ICommandHandler<DeleteShipmentCommand, Unit>
{
    public async Task<Unit> Handle(DeleteShipmentCommand request, CancellationToken cancellationToken)
    {
        var shipment = await shipmentRepository.Get(request.Id, cancellationToken);
        if (shipment is null) throw new NotFoundException(nameof(Shipment), request.Id);

        await shipmentRepository.Delete(request.Id, cancellationToken);
        return Unit.Value;
    }
}
