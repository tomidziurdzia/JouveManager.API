
using JouveManager.Application.CQRS;
using MediatR;

namespace JouveManager.Application.Features.Vehicles.Commands.DeleteVehicle;

public class DeleteVehicleCommand(Guid id) : ICommand<Unit>
{
    public Guid Id { get; set; } = id;
}
