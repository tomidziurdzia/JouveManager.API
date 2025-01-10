using JouveManager.Application.Models.Authorization;
using JouveManager.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace JouveManager.Infrastructure.Data.Extensions;

public class InitialData
{
    public static async Task LoadDataAsync(
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
                var userAdmin = new User
                {
                    FirstName = "Tomas",
                    LastName = "Dziurdzia",
                    UserName = "tomidziurdzia",
                    Email = "tomidziurdzia@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "1234567890",
                    PhoneNumberConfirmed = true,
                    AvatarUrl = "https://firebasestorage.googleapis.com/v0/b/edificacion-app.appspot.com/o/vaxidrez.jpg?alt=media&token=14a28860-d149-461e-9c25-9774d7ac1b24"
                };
                await userManager.CreateAsync(userAdmin, "Walter@960");
                await userManager.AddToRolesAsync(userAdmin, new string[] { Role.Owner });

                var user = new User
                {
                    FirstName = "Ximena",
                    LastName = "Apel",
                    UserName = "ximenaapel",
                    Email = "ximena.apel@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "1234567890",
                    PhoneNumberConfirmed = true,
                    AvatarUrl = "https://firebasestorage.googleapis.com/v0/b/edificacion-app.appspot.com/o/avatar-1.webp?alt=media&token=58da3007-ff21-494d-a85c-25ffa758ff6d"
                };

                await userManager.CreateAsync(user, "Walter@960");
                await userManager.AddToRoleAsync(user, Role.Administrative);
            }

        }
        catch (Exception e)
        {
            var logger = loggerFactory.CreateLogger<InitialData>();
            logger.LogError(e, "An error occurred while seeding the database");
        }
    }
}
