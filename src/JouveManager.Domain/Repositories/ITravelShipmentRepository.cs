namespace JouveManager.Domain.Repositories;

public interface ITravelShipmentRepository
{
    Task<TravelShipment> AssignShipmentToTravel(Guid shipmentId, Guid travelId, CancellationToken cancellationToken);
    Task UnassignShipmentFromTravel(Guid shipmentId, Guid travelId, CancellationToken cancellationToken);
    Task<IEnumerable<TravelShipment>> GetShipmentsByTravelId(Guid travelId, CancellationToken cancellationToken);
    Task<IEnumerable<TravelShipment>> GetTravelsByShipmentId(Guid shipmentId, CancellationToken cancellationToken);
    Task UpdateTravelShipmentAsync(TravelShipment travelShipment, CancellationToken cancellationToken);
    Task<TravelShipment> GetTravelShipment(Guid shipmentId, Guid travelId, CancellationToken cancellationToken);
    Task<IEnumerable<TravelShipment>> GetActiveAssignmentsByShipmentId(Guid shipmentId, CancellationToken cancellationToken);
    Task<Travel> GetTravelWithShipments(Guid travelId, CancellationToken cancellationToken);
    Task<IEnumerable<Travel>> GetTravelsWithShipments(CancellationToken cancellationToken);
}
