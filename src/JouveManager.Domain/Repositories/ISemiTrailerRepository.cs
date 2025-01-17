namespace JouveManager.Domain.Repositories;

public interface ISemiTrailerRepository
{
    Task<IEnumerable<SemiTrailer>> GetAll(CancellationToken cancellationToken);
    Task<SemiTrailer> Get(Guid id, CancellationToken cancellationToken);
    Task Create(SemiTrailer semiTrailer, CancellationToken cancellationToken);
    Task Update(SemiTrailer semiTrailer, CancellationToken cancellationToken);
    Task Delete(Guid id, CancellationToken cancellationToken);
}
