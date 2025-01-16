using JouveManager.Application.Exceptions;
using JouveManager.Domain;
using JouveManager.Domain.Repositories;
using JouveManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JouveManager.Infrastructure.Repositories;

public class VehicleRepository(ApplicationDbContext context) : IVehicleRepository
{
    public async Task Create(Vehicle vehicle, CancellationToken cancellationToken)
    {
        try
        {
            var vehicleExist = await context.Vehicles.FirstOrDefaultAsync(v => v.LicensePlate == vehicle.LicensePlate, cancellationToken);
            if (vehicleExist != null) throw new BadRequestException("Vehicle already exists");

            await context.Vehicles.AddAsync(vehicle, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error creating vehicle: {ex.Message}");
        }
    }

    public async Task<IEnumerable<Vehicle>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            return await context.Vehicles.ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting vehicles: {ex.Message}");
        }
    }

    public async Task<Vehicle> Get(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var vehicle = await context.Vehicles
                .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
            if (vehicle == null) throw new NotFoundException(nameof(Vehicle), id);

            return vehicle;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting vehicle by id: {ex.Message}");
        }
    }

    public async Task Update(Vehicle vehicle, CancellationToken cancellationToken)
    {
        try
        {
            context.Vehicles.Update(vehicle);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error updating vehicle: {ex.Message}");
        }
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var vehicle = await context.Vehicles.FindAsync(id, cancellationToken);
            if (vehicle == null) throw new NotFoundException(nameof(Vehicle), id);

            context.Vehicles.Remove(vehicle);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting vehicle: {ex.Message}");
        }
    }
}
