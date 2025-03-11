using FluentValidation.Results;
using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.Travel;
using JouveManager.Application.Exceptions;
using JouveManager.Domain;
using JouveManager.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace JouveManager.Application.Features.Travels.Commands.CreateTravel;

public class CreateTravelCommandHandler(
    UserManager<User> userManager,
    ITravelRepository travelRepository,
    IVehicleRepository vehicleRepository,
    ISemiTrailerRepository semiTrailerRepository) : ICommandHandler<CreateTravelCommand, TravelDto>
{
    public async Task<TravelDto> Handle(CreateTravelCommand request, CancellationToken cancellationToken)
    {
        var driver = await userManager.FindByIdAsync(request.DriverId)
            ?? throw new NotFoundException("Driver", request.DriverId);

        var isDriverRole = await userManager.IsInRoleAsync(driver, "Driver");
        if (!isDriverRole)
        {
            throw new ValidationException(new[]
            {
                new ValidationFailure("DriverId", $"The user with ID {request.DriverId} does not have the role 'Driver'.")
            });
        }

        User? assistant = null;
        if (request.AssistantId != null)
        {
            assistant = await userManager.FindByIdAsync(request.AssistantId)
                ?? throw new NotFoundException("Assistant", request.AssistantId);

            var isAssistantRole = await userManager.IsInRoleAsync(assistant, "Assistant");
            if (!isAssistantRole)
            {
                throw new ValidationException(new[]
                {
                    new ValidationFailure("AssistantId", $"The user with ID {request.AssistantId} does not have the role 'Assistant'.")
                });
            }

            if (request.DriverId == request.AssistantId)
            {
                throw new ValidationException(new[]
                {
                    new ValidationFailure("AssistantId", "The Driver and Assistant cannot be the same person.")
                });
            }
        }

        var vehicle = await vehicleRepository.Get(request.VehicleId, cancellationToken)
            ?? throw new NotFoundException("Vehicle", request.VehicleId);

        var semiTrailer = request.SemiTrailerId != null
            ? await semiTrailerRepository.Get(request.SemiTrailerId.Value, cancellationToken)
            : null;

        var travel = new Travel
        {
            Date = request.Date,
            DriverId = request.DriverId.ToString(),
            AssistantId = request.AssistantId?.ToString(),
            VehicleId = request.VehicleId,
            SemiTrailerId = request.SemiTrailerId
        };
        await travelRepository.Create(travel, cancellationToken);

        var driverFullname = $"{driver.FirstName} {driver.LastName}";
        var assistantFullname = assistant != null ? $"{assistant.FirstName} {assistant.LastName}" : string.Empty;

        return new TravelDto
        {
            Id = travel.Id,
            Date = travel.Date,
            DriverId = new Guid(travel.DriverId),
            DriverName = driverFullname,
            AssistantId = travel.AssistantId != null ? new Guid(travel.AssistantId) : null,
            AssistantName = assistantFullname,
            VehicleId = travel.VehicleId,
            VehicleLicensePlate = vehicle?.LicensePlate ?? string.Empty,
            SemiTrailerId = travel.SemiTrailerId != null ? travel.SemiTrailerId.Value : null,
            SemiTrailerLicensePlate = semiTrailer?.LicensePlate ?? string.Empty
        };
    }
}
