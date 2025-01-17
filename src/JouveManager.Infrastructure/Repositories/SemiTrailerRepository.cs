using JouveManager.Application.Exceptions;
using JouveManager.Domain;
using JouveManager.Domain.Repositories;
using JouveManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JouveManager.Infrastructure.Repositories;

public class SemiTrailerRepository(ApplicationDbContext context) : ISemiTrailerRepository
{
    public async Task Create(SemiTrailer semiTrailer, CancellationToken cancellationToken)
    {
        try
        {
            var semiTrailerExist = await context.SemiTrailers.FirstOrDefaultAsync(v => v.LicensePlate == semiTrailer.LicensePlate, cancellationToken);
            if (semiTrailerExist != null) throw new BadRequestException("SemiTrailer already exists");

            await context.SemiTrailers.AddAsync(semiTrailer, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error creating semiTrailer: {ex.Message}");
        }
    }

    public async Task<IEnumerable<SemiTrailer>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            return await context.SemiTrailers.ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting semiTrailers: {ex.Message}");
        }
    }

    public async Task<SemiTrailer> Get(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var semiTrailer = await context.SemiTrailers
                .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
            if (semiTrailer == null) throw new NotFoundException(nameof(SemiTrailer), id);

            return semiTrailer;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting semiTrailer by id: {ex.Message}");
        }
    }

    public async Task Update(SemiTrailer semiTrailer, CancellationToken cancellationToken)
    {
        try
        {
            context.SemiTrailers.Update(semiTrailer);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error updating semiTrailer: {ex.Message}");
        }
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var semiTrailer = await context.SemiTrailers.FindAsync(id, cancellationToken);
            if (semiTrailer == null) throw new NotFoundException(nameof(SemiTrailer), id);

            context.SemiTrailers.Remove(semiTrailer);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting semiTrailer: {ex.Message}");
        }
    }
}