using JouveManager.Application.Models.Authorization;
using JouveManager.Domain;
using JouveManager.Infrastructure.Data.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace JouveManager.Infrastructure.Data.Extensions;

public class InitialData
{
    public static async Task LoadDataAsync(
        ApplicationDbContext context,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        ILoggerFactory loggerFactory
    )
    {
        try
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(Role.Owner));
                await roleManager.CreateAsync(new IdentityRole(Role.Manager));
                await roleManager.CreateAsync(new IdentityRole(Role.Administrative));
                await roleManager.CreateAsync(new IdentityRole(Role.Driver));
                await roleManager.CreateAsync(new IdentityRole(Role.Assistant));
            }

            if (!userManager.Users.Any())
            {

                var userData = File.ReadAllText("../JouveManager.Infrastructure/Data/Extensions/Json/user.json");
                var users = JsonConvert.DeserializeObject<List<UserSeedModel>>(userData);

                if (users != null)
                {
                    foreach (var userSeed in users)
                    {
                        var user = new User
                        {
                            FirstName = userSeed.FirstName,
                            LastName = userSeed.LastName,
                            UserName = userSeed.UserName,
                            Email = userSeed.Email
                        };

                        var result = await userManager.CreateAsync(user, userSeed.Password);
                        if (result.Succeeded)
                        {
                            if (userSeed.Roles != null && userSeed.Roles.Any())
                            {
                                await userManager.AddToRolesAsync(user, userSeed.Roles);
                            }
                        }
                        else
                        {
                            var logger = loggerFactory.CreateLogger<InitialData>();
                            logger.LogError("Error creando el usuario {UserName}: {Errors}",
                                user.UserName,
                                string.Join(", ", result.Errors.Select(e => e.Description)));
                        }
                    }
                }
            }

            if (!context.Vehicles.Any())
            {
                var vehicleData = File.ReadAllText("../JouveManager.Infrastructure/Data/Extensions/Json/vehicle.json");
                    
                var vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(vehicleData);
                    
                if (vehicles != null)
                {
                    await context.Vehicles.AddRangeAsync(vehicles);
                    await context.SaveChangesAsync();
                }
            }
            
            if (!context.SemiTrailers.Any())
            {
                var semiTrailersData = File.ReadAllText("../JouveManager.Infrastructure/Data/Extensions/Json/semi-trailer.json");
                    
                var semiTrailers = JsonConvert.DeserializeObject<List<SemiTrailer>>(semiTrailersData);
                    
                if (semiTrailers != null)
                {
                    await context.SemiTrailers.AddRangeAsync(semiTrailers);
                    await context.SaveChangesAsync();
                }
            }
            
            if (!context.Shipments.Any())
            {
                var shipmentData = File.ReadAllText("../JouveManager.Infrastructure/Data/Extensions/Json/shipment.json");
                    
                var shipments = JsonConvert.DeserializeObject<List<Shipment>>(shipmentData);
                    
                if (shipments != null)
                {
                    await context.Shipments.AddRangeAsync(shipments);
                    await context.SaveChangesAsync();
                }
            }

        }
        catch (Exception e)
        {
            var logger = loggerFactory.CreateLogger<InitialData>();
            logger.LogError(e, "An error occurred while seeding the database");
        }
    }
}

public class UserSeedModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<string> Roles { get; set; }
}