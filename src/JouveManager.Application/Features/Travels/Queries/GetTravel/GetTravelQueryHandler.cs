using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.Travel;
using JouveManager.Application.Exceptions;
using JouveManager.Domain;
using JouveManager.Domain.Repositories;

namespace JouveManager.Application.Features.Travels.Queries.GetTravel;

public class GetTravelQueryHandler(ITravelRepository travelRepository) : IQueryHandler<GetTravelQuery, TravelDto>
{
    public async Task<TravelDto> Handle(GetTravelQuery request, CancellationToken cancellationToken)
    {
        var travel = await travelRepository.Get(request.Id, cancellationToken);
        if (travel is null)
        {
            throw new NotFoundException(nameof(Travel), request.Id);
        }

        return new TravelDto
        {
            Id = travel.Id,
            Date = travel.Date,
            DriverId = new Guid(travel.DriverId),
            DriverName = $"{travel.Driver.FirstName} {travel.Driver.LastName}",
            AssistantId = travel.AssistantId != null ? new Guid(travel.AssistantId) : null,
            AssistantName = travel.AssistantId != null ? $"{travel.Assistant.FirstName} {travel.Assistant.LastName}" : string.Empty,
            VehicleId = travel.VehicleId,
            VehicleLicensePlate = travel.Vehicle.LicensePlate,
            SemiTrailerId = travel.SemiTrailerId,
            SemiTrailerLicensePlate = travel.SemiTrailerId != null ? travel.SemiTrailer.LicensePlate : string.Empty
        };
    }
}

