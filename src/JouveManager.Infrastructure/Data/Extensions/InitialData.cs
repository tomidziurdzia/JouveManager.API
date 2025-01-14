using JouveManager.Application.Models.Authorization;
using JouveManager.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace JouveManager.Infrastructure.Data.Extensions;

public class InitialData
{
    public static async Task LoadDataAsync(
        UserManager<User> userManager,
        //RoleManager<IdentityRole> roleManager,
        ILoggerFactory loggerFactory
    )
    {
        try
        {
            /*if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(Role.Owner));
                await roleManager.CreateAsync(new IdentityRole(Role.Manager));
                await roleManager.CreateAsync(new IdentityRole(Role.Administrative));
                await roleManager.CreateAsync(new IdentityRole(Role.Driver));
                await roleManager.CreateAsync(new IdentityRole(Role.Assistant));
            }*/

            if (!userManager.Users.Any())
            {
                var userAdmin = new User
                {
                    FirstName = "Tomas",
                    LastName = "Dziurdzia",
                    UserName = "tomidziurdzia",
                    Email = "tomidziurdzia@gmail.com"
                };
                await userManager.CreateAsync(userAdmin, "Walter@960");
                //await userManager.AddToRolesAsync(userAdmin, new string[] { Role.Owner });

                var user = new User
                {
                    FirstName = "Xime",
                    LastName = "Apel",
                    UserName = "ximeapel",
                    Email = "ximeapel@gmail.com"
                };

                await userManager.CreateAsync(user, "Walter@960");
                //await userManager.AddToRoleAsync(user, Role.Administrative);
            }

        }
        catch (Exception e)
        {
            var logger = loggerFactory.CreateLogger<InitialData>();
            logger.LogError(e, "An error occurred while seeding the database");
        }
    }
}
