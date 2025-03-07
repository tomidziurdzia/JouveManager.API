using JouveManager.Domain;

namespace JouveManager.Application.Contracts.Identity;

public interface IAuthService
{
    string GetSessionUser();
    string CreateToken(User user, IList<string> roles);
    Task<string> GetUserFullName(string username);
}

