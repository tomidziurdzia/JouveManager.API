using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.Travel;
using JouveManager.Domain.Repositories;

namespace JouveManager.Application.Features.Travels.Queries.GetTravelsByDate;

public class GetTravelsByDateQueryHandler(ITravelRepository travelRepository) : IQueryHandler<GetTravelsByDateQuery, IEnumerable<TravelDto>>
{
    public async Task<IEnumerable<TravelDto>> Handle(GetTravelsByDateQuery request, CancellationToken cancellationToken)
    {
        var travels = await travelRepository.GetTravelsByDate(request.Date, cancellationToken);

        return travels.Select(travel => new TravelDto
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
        }).ToList();
    }
}
