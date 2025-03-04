using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.TravelShipmentHistory;
using JouveManager.Domain.Repositories;
using MediatR;

namespace JouveManager.Application.Features.TravelShipmentHistory.Queries.GetTravelShipmentHistory;

public class GetTravelShipmentHistoryQueryHandler(ITravelShipmentHistoryRepository travelShipmentHistoryRepository) : IQueryHandler<GetTravelShipmentHistoryQuery, IEnumerable<TravelShipmentHistoryDto>>
{
    public async Task<IEnumerable<TravelShipmentHistoryDto>> Handle(GetTravelShipmentHistoryQuery request, CancellationToken cancellationToken)
    {
        var travelShipmentHistory = await travelShipmentHistoryRepository.GetTravelShipmentHistory(request.ShipmentId, cancellationToken);
        return travelShipmentHistory.Select(history => new TravelShipmentHistoryDto
        {
            Id = history.Id,
            OldStatus = history.OldStatus,
            NewStatus = history.NewStatus,
            Comments = history.Comments,
            CreatedAt = history.CreatedAt,
            CreatedBy = history.CreatedBy,
            LastModified = history.LastModified,
            LastModifiedBy = history.LastModifiedBy
        }).ToList();
    }
}
