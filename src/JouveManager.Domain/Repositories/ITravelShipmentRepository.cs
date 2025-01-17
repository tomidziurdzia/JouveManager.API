namespace JouveManager.Domain.Repositories;

public interface ITravelShipmentRepository
{
    Task AssignShipmentToTravel(Guid shipmentId, Guid travelId, CancellationToken cancellationToken);
    Task<IEnumerable<TravelShipment>> GetShipmentsByTravelId(Guid travelId, CancellationToken cancellationToken);
    Task<IEnumerable<TravelShipment>> GetTravelsByShipmentId(Guid shipmentId, CancellationToken cancellationToken);
}
