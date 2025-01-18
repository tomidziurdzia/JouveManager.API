using JouveManager.Application.CQRS;
using JouveManager.Application.Exceptions;
using JouveManager.Application.Features.Travels.Commands.UpdateTravel;
using JouveManager.Domain;
using JouveManager.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class UpdateTravelCommandHandler(
    UserManager<User> userManager,
    ITravelRepository travelRepository,
    IVehicleRepository vehicleRepository,
    ISemiTrailerRepository semiTrailerRepository
) : ICommandHandler<UpdateTravelCommand, Unit>
{
    public async Task<Unit> Handle(UpdateTravelCommand request, CancellationToken cancellationToken)
    {
        var travel = await travelRepository.Get(request.Id, cancellationToken);
        if (travel is null)
        {
            throw new NotFoundException("Travel", request.Id);
        }

        if (request.Date.HasValue)
        {
            travel.Date = request.Date.Value;
        }

        if (!string.IsNullOrEmpty(request.DriverId))
        {
            var driver = await userManager.FindByIdAsync(request.DriverId)
                ?? throw new NotFoundException("Driver", request.DriverId);

            var isDriverRole = await userManager.IsInRoleAsync(driver, "Driver");
            if (!isDriverRole)
            {
                throw new ValidationException(new[]
                {
                    new FluentValidation.Results.ValidationFailure("DriverId", "The selected user does not have the role 'Driver'.")
                });
            }

            travel.DriverId = request.DriverId;
        }

        if (!string.IsNullOrEmpty(request.AssistantId))
        {
            var assistant = await userManager.FindByIdAsync(request.AssistantId)
                ?? throw new NotFoundException("Assistant", request.AssistantId);

            var isAssistantRole = await userManager.IsInRoleAsync(assistant, "Assistant");
            if (!isAssistantRole)
            {
                throw new ValidationException(new[]
                {
                    new FluentValidation.Results.ValidationFailure("AssistantId", "The selected user does not have the role 'Assistant'.")
                });
            }

            if (request.DriverId == request.AssistantId)
            {
                throw new ValidationException(new[]
                {
                    new FluentValidation.Results.ValidationFailure("DriverId, AssistantId", "The Driver and Assistant cannot be the same person.")
                });
            }

            travel.AssistantId = request.AssistantId;
        }

        if (request.VehicleId.HasValue)
        {
            var vehicle = await vehicleRepository.Get(request.VehicleId.Value, cancellationToken)
                ?? throw new NotFoundException("Vehicle", request.VehicleId);

            travel.VehicleId = request.VehicleId.Value;
        }

        if (request.SemiTrailerId.HasValue)
        {
            var semiTrailer = await semiTrailerRepository.Get(request.SemiTrailerId.Value, cancellationToken)
                ?? throw new NotFoundException("SemiTrailer", request.SemiTrailerId);

            travel.SemiTrailerId = request.SemiTrailerId;
        }

        await travelRepository.Update(travel, cancellationToken);

        return Unit.Value;
    }
}
