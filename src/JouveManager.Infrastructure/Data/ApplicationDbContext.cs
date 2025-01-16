using JouveManager.Application.Data;
using JouveManager.Domain;
using JouveManager.Domain.Abstractions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JouveManager.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<User>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        var userName = "system";

        foreach (var entry in ChangeTracker.Entries<Entity<Guid>>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.Now;
                    entry.Entity.CreatedBy = userName;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModified = DateTime.Now;
                    entry.Entity.LastModifiedBy = userName;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    public required DbSet<Vehicle> Vehicles { get; set; }
    public required DbSet<SemiTrailer> SemiTrailers { get; set; }
    public required DbSet<Travel> Travels { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Travel>()
                .HasOne(t => t.Driver)
                .WithMany()
                .HasForeignKey(t => t.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Travel>()
            .HasOne(t => t.Assistant)
            .WithMany()
            .HasForeignKey(t => t.AssistantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Travel>()
            .HasOne(t => t.Vehicle)
            .WithMany(v => v.Travels)
            .HasForeignKey(t => t.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Travel>()
            .HasOne(t => t.SemiTrailer)
            .WithMany(s => s.Travels)
            .HasForeignKey(t => t.SemiTrailerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
