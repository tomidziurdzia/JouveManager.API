using JouveManager.Application.Exceptions;
using JouveManager.Domain;
using JouveManager.Domain.Repositories;
using JouveManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JouveManager.Infrastructure.Repositories;

public class ShipmentRepository(ApplicationDbContext context) : IShipmentRepository
{
    public async Task Create(Shipment shipment, CancellationToken cancellationToken)
    {
        try
        {
            await context.Shipments.AddAsync(shipment, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error creating shipment: {ex.Message}");
        }
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var shipment = await context.Shipments.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
            if (shipment == null) throw new NotFoundException(nameof(Shipment), id);

            context.Shipments.Remove(shipment);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting shipment: {ex.Message}");
        }
    }

    public async Task<Shipment> Get(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var shipment = await context.Shipments
                .Include(s => s.TravelShipments)
                .ThenInclude(ts => ts.Travel)
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
            if (shipment == null) throw new NotFoundException(nameof(Shipment), id);

            return shipment;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting shipment by id: {ex.Message}");
        }
    }

    public async Task<IEnumerable<Shipment>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            return await context.Shipments
                .Include(s => s.TravelShipments)
                .ThenInclude(ts => ts.Travel)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting shipments: {ex.Message}");
        }
    }

    public async Task Update(Shipment shipment, CancellationToken cancellationToken)
    {
        try
        {
            context.Shipments.Update(shipment);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error updating shipment: {ex.Message}");
        }
    }

    public async Task<IEnumerable<Shipment>> GetShipmentsByDate(DateTime date, CancellationToken cancellationToken)
    {
        try
        {
            return await context.Shipments
                .Where(s => s.ScheduledDate.Date == date.Date)
                .OrderByDescending(s => s.ScheduledDate)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting shipments by date: {ex.Message}");
        }
    }
}
