using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JouveManager.Application.Contracts.Identity;
using JouveManager.Application.Models.Token;
using JouveManager.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JouveManager.Infrastructure.Services.Auth;

public class AuthService(
    IHttpContextAccessor httpContextAccessor,
    IOptions<JwtSettings> jwtSettings,
    UserManager<User> userManager) : IAuthService
{
    private JwtSettings _jwtSettings { get; } = jwtSettings.Value;

    public string CreateToken(User user, IList<string> roles)
    {
        var claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.NameId, user.UserName!),
            new Claim("userId", user.Id),
            new Claim("email", user.Email!),
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescription = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(_jwtSettings.ExpireTime),
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescription);
        return tokenHandler.WriteToken(token);
    }

    public string GetSessionUser()
    {
        var username = httpContextAccessor.HttpContext!.User?.Claims?
            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        return username!;
    }

    public async Task<string> GetUserFullName(string username)
    {
        var user = await userManager.FindByEmailAsync(username);
        return user != null ? $"{user.FirstName} {user.LastName}".Trim() : username;
    }
}
