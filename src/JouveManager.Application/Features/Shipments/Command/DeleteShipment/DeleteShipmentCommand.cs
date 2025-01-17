using JouveManager.Application.CQRS;
using MediatR;

namespace JouveManager.Application.Features.Shipments.Command.DeleteShipment;

public class DeleteShipmentCommand(Guid id) : ICommand<Unit>
{
    public Guid Id { get; set; } = id;
}

