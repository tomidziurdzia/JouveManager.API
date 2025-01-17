using JouveManager.Application.CQRS;
using JouveManager.Application.Exceptions;
using JouveManager.Domain;
using JouveManager.Domain.Repositories;
using MediatR;

namespace JouveManager.Application.Features.Vehicles.Commands.DeleteVehicle;

public class DeleteVehicleCommandHandler(IVehicleRepository vehicleRepository) : ICommandHandler<DeleteVehicleCommand, Unit>
{
    public async Task<Unit> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
    {
        var vehicle = await vehicleRepository.Get(request.Id, cancellationToken);
        if (vehicle is null) throw new NotFoundException(nameof(Vehicle), request.Id);

        await vehicleRepository.Delete(vehicle.Id, cancellationToken);
        return Unit.Value;
    }
}
