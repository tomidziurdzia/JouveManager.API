using JouveManager.Application.Contracts.Identity;
using JouveManager.Application.Models.Token;
using JouveManager.Infrastructure.Data;
using JouveManager.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JouveManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IAuthService, AuthService>();

        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
            )
        );
        
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        return services;
    }
}
