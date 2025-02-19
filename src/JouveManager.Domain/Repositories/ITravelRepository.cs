namespace JouveManager.Domain.Repositories;

public interface ITravelRepository
{
    Task<IEnumerable<Travel>> GetAll(CancellationToken cancellationToken);
    Task<Travel> Get(Guid id, CancellationToken cancellationToken);
    Task Create(Travel travel, CancellationToken cancellationToken);
    Task Update(Travel travel, CancellationToken cancellationToken);
    Task Delete(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Travel>> GetTravelsByDate(DateTime date, CancellationToken cancellationToken);
}