namespace JouveManager.Domain.Repositories;

public interface ITravelShipmentHistoryRepository
{
    Task Create(TravelShipmentHistory travelShipmentHistory, CancellationToken cancellationToken);
    Task<IEnumerable<TravelShipmentHistory>> GetTravelShipmentHistories(Guid travelShipmentId, CancellationToken cancellationToken);
}