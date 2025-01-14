using JouveManager.Application.Contracts.Identity;
using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.User;
using JouveManager.Domain;
using Microsoft.AspNetCore.Identity;

namespace JouveManager.Application.Features.Auths.Users.Queries.GetUserByToken;

public class GetUserByTokenHandler(UserManager<User> userManager, IAuthService authService) : IQueryHandler<GetUserByToken, AuthResponseDto>
{
    public async Task<AuthResponseDto> Handle(GetUserByToken request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(authService.GetSessionUser());
        if(user is null)
        {
            throw new Exception("User is not authenticated");
        }
        
        var roles = await userManager.GetRolesAsync(user);
        
        var token = authService.CreateToken(user, roles);
        
        return new AuthResponseDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            UserName = user.UserName!,
            Token = token,
            Roles = roles
        };
    }
}