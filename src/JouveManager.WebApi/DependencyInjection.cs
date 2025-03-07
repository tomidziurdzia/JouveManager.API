using HealthChecks.UI.Client;
using JouveManager.Application.Exceptions.Handler;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace JouveManager.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddExceptionHandler<CustomExceptionHandler>();

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {

        app.UseExceptionHandler(options => { });
       
        return app;
    }
}
