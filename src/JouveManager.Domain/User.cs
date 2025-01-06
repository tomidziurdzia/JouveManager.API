using Microsoft.AspNetCore.Identity;

namespace JouveManager.Domain;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string Avatar { get; set; } = string.Empty;
    public ICollection<UserType> UserTypes { get; set; }
}