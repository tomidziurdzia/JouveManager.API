namespace JouveManager.Domain.Repositories;

public interface IShipmentRepository
{
    Task<IEnumerable<Shipment>> GetAll(CancellationToken cancellationToken);
    Task<Shipment> Get(Guid id, CancellationToken cancellationToken);
    Task Create(Shipment shipment, CancellationToken cancellationToken);
    Task Update(Shipment shipment, CancellationToken cancellationToken);
    Task Delete(Guid id, CancellationToken cancellationToken); 
    Task<IEnumerable<Shipment>> GetShipmentsByDate(DateTime requestDate, CancellationToken cancellationToken);
}
