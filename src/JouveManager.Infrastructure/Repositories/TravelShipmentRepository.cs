using JouveManager.Domain.Repositories;
using JouveManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JouveManager.Infrastructure.Repositories;

public class TravelShipmentRepository(ApplicationDbContext context) : ITravelShipmentRepository
{
    public async Task AssignShipmentToTravel(Guid shipmentId, Guid travelId, CancellationToken cancellationToken)
    {
        try
        {
            var travelShipment = new TravelShipment { ShipmentId = shipmentId, TravelId = travelId };
            context.TravelShipments.Add(travelShipment);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error assigning shipment to travel: {ex.Message}");
        }
    }

    public async Task<IEnumerable<TravelShipment>> GetShipmentsByTravelId(Guid travelId, CancellationToken cancellationToken)
    {
        try
        {
            return await context.TravelShipments.Where(ts => ts.TravelId == travelId).ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting shipments by travel id: {ex.Message}");
        }
    }

    public async Task<IEnumerable<TravelShipment>> GetTravelsByShipmentId(Guid shipmentId, CancellationToken cancellationToken)
    {
        try
        {
            return await context.TravelShipments.Where(ts => ts.ShipmentId == shipmentId).ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting travels by shipment id: {ex.Message}");
        }
    }
}
