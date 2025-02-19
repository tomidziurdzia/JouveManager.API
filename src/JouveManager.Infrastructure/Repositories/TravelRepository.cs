using JouveManager.Application.Exceptions;
using JouveManager.Domain;
using JouveManager.Domain.Repositories;
using JouveManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JouveManager.Infrastructure.Repositories;

public class TravelRepository(ApplicationDbContext context) : ITravelRepository
{
    public async Task Create(Travel travel, CancellationToken cancellationToken)
    {
        try
        {
            await context.Travels.AddAsync(travel, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error creating travel: {ex.Message}");
        }
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var travel = await context.Travels.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
            if (travel == null) throw new NotFoundException(nameof(Travel), id);

            context.Travels.Remove(travel);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting travel: {ex.Message}");
        }
    }

    public async Task<Travel> Get(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var travel = await context.Travels
                .Include(t => t.Driver)
                .Include(t => t.Assistant)
                .Include(t => t.Vehicle)
                .Include(t => t.SemiTrailer)
                .Include(t => t.TravelShipments)
                .ThenInclude(ts => ts.Shipment)
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
            if (travel == null) throw new NotFoundException(nameof(Travel), id);

            return travel;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting travel: {ex.Message}");
        }
    }

    public async Task<IEnumerable<Travel>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            return await context.Travels
                .Include(t => t.Driver)
                .Include(t => t.Assistant)
                .Include(t => t.Vehicle)
                .Include(t => t.SemiTrailer)
                .Include(t => t.TravelShipments)
                .ThenInclude(ts => ts.Shipment)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting travels: {ex.Message}");
        }
    }

    public async Task Update(Travel travel, CancellationToken cancellationToken)
    {
        try
        {
            context.Travels.Update(travel);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error updating travel: {ex.Message}");
        }
    }

    public async Task<IEnumerable<Travel>> GetTravelsByDate(DateTime date, CancellationToken cancellationToken)
    {
        try
        {
            return await context.Travels
                .Include(t => t.Driver)
                .Include(t => t.Assistant)
                .Include(t => t.Vehicle)
                .Include(t => t.SemiTrailer)
                .Include(t => t.TravelShipments)
                .ThenInclude(ts => ts.Shipment)
                .Where(t => t.Date.Date == date.Date)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting travels by date: {ex.Message}");
        }
    }
}
