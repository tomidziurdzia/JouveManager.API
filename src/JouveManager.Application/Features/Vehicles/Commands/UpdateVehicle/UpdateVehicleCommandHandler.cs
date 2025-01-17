using JouveManager.Application.CQRS;
using JouveManager.Application.Exceptions;
using JouveManager.Domain;
using JouveManager.Domain.Repositories;
using MediatR;

namespace JouveManager.Application.Features.Vehicles.Commands.UpdateVehicle;

public class UpdateVehicleCommandHandler(IVehicleRepository vehicleRepository) : ICommandHandler<UpdateVehicleCommand, Unit>
{
    public async Task<Unit> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
    {
        var vehicle = await vehicleRepository.Get(request.Id, cancellationToken);
        if (vehicle is null) throw new NotFoundException(nameof(Vehicle), request.Id);

        vehicle.LicensePlate = request.LicensePlate ?? vehicle.LicensePlate;
        vehicle.Brand = request.Brand ?? vehicle.Brand;
        vehicle.Model = request.Model ?? vehicle.Model;
        vehicle.ImageUrl = request.ImageUrl ?? vehicle.ImageUrl;
        vehicle.Type = request.Type ?? vehicle.Type;

        await vehicleRepository.Update(vehicle, cancellationToken);
        return Unit.Value;
    }
}
