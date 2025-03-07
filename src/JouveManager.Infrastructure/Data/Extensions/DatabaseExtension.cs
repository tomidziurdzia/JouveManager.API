using JouveManager.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace JouveManager.Infrastructure.Data.Extensions;

public static class DatabaseExtension
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.Database.MigrateAsync();
        await SeedAsync(scope.ServiceProvider);
    }

    private static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        await SeedUserAsync(context, userManager, roleManager, loggerFactory);
    }

    private static async Task SeedUserAsync(ApplicationDbContext context,
        UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ILoggerFactory loggerFactory
    )
    {
        await InitialData.LoadDataAsync(context, userManager, roleManager, loggerFactory);
    }
}