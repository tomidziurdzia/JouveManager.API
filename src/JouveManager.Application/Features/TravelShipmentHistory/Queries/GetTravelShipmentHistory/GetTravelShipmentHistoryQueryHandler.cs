using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.TravelShipmentHistory;
using JouveManager.Domain.Repositories;
using JouveManager.Application.Contracts.Identity;

namespace JouveManager.Application.Features.TravelShipmentHistory.Queries.GetTravelShipmentHistory;

public class GetTravelShipmentHistoryQueryHandler(
    ITravelShipmentHistoryRepository travelShipmentHistoryRepository,
    IAuthService authService) : IQueryHandler<GetTravelShipmentHistoryQuery, IEnumerable<TravelShipmentHistoryDto>>
{
    public async Task<IEnumerable<TravelShipmentHistoryDto>> Handle(GetTravelShipmentHistoryQuery request,
        CancellationToken cancellationToken)
    {
        var travelShipmentHistory = await travelShipmentHistoryRepository.GetTravelShipmentHistory(request.ShipmentId, cancellationToken);
        var result = new List<TravelShipmentHistoryDto>();

        foreach (var history in travelShipmentHistory)
        {
            var fullName = await authService.GetUserFullName(history.LastModifiedBy);

            result.Add(new TravelShipmentHistoryDto
            {
                Id = history.Id,
                OldStatus = history.OldStatus,
                NewStatus = history.NewStatus,
                Comments = history.Comments,
                CreatedAt = history.CreatedAt,
                CreatedBy = history.CreatedBy,
                LastModified = history.LastModified,
                LastModifiedBy = fullName
            });
        }

        return result;
    }
}
