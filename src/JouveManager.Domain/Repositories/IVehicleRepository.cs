namespace JouveManager.Domain.Repositories;

public interface IVehicleRepository
{
    Task<IEnumerable<Vehicle>> GetAll(CancellationToken cancellationToken);
    Task<Vehicle> Get(Guid id, CancellationToken cancellationToken);
    Task Create(Vehicle vehicle, CancellationToken cancellationToken);
    Task Update(Vehicle vehicle, CancellationToken cancellationToken);
    Task Delete(Guid id, CancellationToken cancellationToken);
}
