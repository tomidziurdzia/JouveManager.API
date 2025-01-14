using Microsoft.AspNetCore.Identity;

namespace JouveManager.Domain;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string AvatarUrl { get; set; } = string.Empty;
}