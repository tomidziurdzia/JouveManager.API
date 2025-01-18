using JouveManager.Domain;
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
            var exists = await context.TravelShipments.AnyAsync(ts => ts.ShipmentId == shipmentId && ts.TravelId == travelId, cancellationToken);

            if (exists) throw new InvalidOperationException("This shipment is already assigned to the specified travel.");

            var travelShipment = new TravelShipment
            {
                ShipmentId = shipmentId,
                TravelId = travelId
            };

            context.TravelShipments.Add(travelShipment);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error assigning shipment to travel: {ex.Message}");
        }
    }

    public async Task UnassignShipmentFromTravel(Guid shipmentId, Guid travelId, CancellationToken cancellationToken)
    {
        var travelShipment = await context.TravelShipments
            .FirstOrDefaultAsync(ts => ts.ShipmentId == shipmentId && ts.TravelId == travelId, cancellationToken);

        if (travelShipment == null)
        {
            throw new InvalidOperationException("The shipment is not assigned to the specified travel.");
        }

        context.TravelShipments.Remove(travelShipment);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<TravelShipment>> GetShipmentsByTravelId(Guid travelId, CancellationToken cancellationToken)
    {
        try
        {
            return await context.TravelShipments
                        .Include(ts => ts.Shipment)
                        .Where(ts => ts.TravelId == travelId)
                        .ToListAsync(cancellationToken);
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
            return await context.TravelShipments
                        .Include(ts => ts.Travel)
                        .Where(ts => ts.ShipmentId == shipmentId)
                        .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting travels by shipment id: {ex.Message}");
        }
    }

    public async Task UpdateTravelShipmentAsync(TravelShipment travelShipment, CancellationToken cancellationToken)
    {
        context.TravelShipments.Update(travelShipment);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<TravelShipment> GetTravelShipment(Guid shipmentId, Guid travelId, CancellationToken cancellationToken)
    {
        var travelShipment = await context.TravelShipments
            .FirstOrDefaultAsync(ts => ts.ShipmentId == shipmentId && ts.TravelId == travelId, cancellationToken);

        if (travelShipment == null)
        {
            throw new Exception("TravelShipment not found");
        }

        return travelShipment;
    }

    public async Task<IEnumerable<TravelShipment>> GetActiveAssignmentsByShipmentId(Guid shipmentId, CancellationToken cancellationToken)
    {
        return await context.TravelShipments
            .Where(ts => ts.ShipmentId == shipmentId && !ts.Delivered)
            .ToListAsync(cancellationToken);
    }

    public async Task<Travel> GetTravelWithShipments(Guid travelId, CancellationToken cancellationToken)
    {
        return await context.Travels
        .Include(t => t.Driver)
        .Include(t => t.Assistant)
        .Include(t => t.Vehicle)
        .Include(t => t.SemiTrailer)
        .Include(t => t.TravelShipments)
        .ThenInclude(ts => ts.Shipment)
        .FirstOrDefaultAsync(t => t.Id == travelId, cancellationToken)
        ?? throw new Exception($"Travel with ID {travelId} not found.");
    }

    public async Task<IEnumerable<Travel>> GetTravelsWithShipments(CancellationToken cancellationToken)
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
}