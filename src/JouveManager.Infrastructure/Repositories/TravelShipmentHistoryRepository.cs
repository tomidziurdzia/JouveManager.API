using System;
using JouveManager.Domain;
using JouveManager.Domain.Repositories;
using JouveManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JouveManager.Infrastructure.Repositories;

public class TravelShipmentHistoryRepository(ApplicationDbContext context) : ITravelShipmentHistoryRepository
{
    public async Task Create(TravelShipmentHistory travelShipmentHistory, CancellationToken cancellationToken)
    {
        try
        {
            await context.TravelShipmentHistories.AddAsync(travelShipmentHistory, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting travel: {ex.Message}");

        }

    }

    public async Task<IEnumerable<TravelShipmentHistory>> GetTravelShipmentHistories(Guid travelShipmentId, CancellationToken cancellationToken)
    {
        return await context.TravelShipmentHistories.Where(x => x.TravelShipmentId == travelShipmentId).OrderByDescending(x => x.LastModified).ToListAsync(cancellationToken);
    }
}
