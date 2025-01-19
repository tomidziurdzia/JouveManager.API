using JouveManager.Application.Contracts.Identity;
using JouveManager.Application.Models.Token;
using JouveManager.Domain;
using JouveManager.Domain.Repositories;
using JouveManager.Infrastructure.Data;
using JouveManager.Infrastructure.Repositories;
using JouveManager.Infrastructure.Services.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JouveManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IVehicleRepository, VehicleRepository>();
        services.AddTransient<ISemiTrailerRepository, SemiTrailerRepository>();
        services.AddTransient<IShipmentRepository, ShipmentRepository>();
        services.AddTransient<ITravelRepository, TravelRepository>();
        services.AddTransient<ITravelShipmentRepository, TravelShipmentRepository>();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
            ));
            
        services.AddIdentityCore<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager<SignInManager<User>>()
            .AddDefaultTokenProviders()
            .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<User, IdentityRole>>();

        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        return services;
    }
}
