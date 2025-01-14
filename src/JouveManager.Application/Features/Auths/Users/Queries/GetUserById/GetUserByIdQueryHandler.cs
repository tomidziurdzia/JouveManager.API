using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.User;
using JouveManager.Domain;
using Microsoft.AspNetCore.Identity;

namespace JouveManager.Application.Features.Auths.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler(UserManager<User> userManager) : IQueryHandler<GetUserByIdQuery, AuthResponseDto>
{
    public async Task<AuthResponseDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Id!);
        if (user is null)
        {
            throw new Exception("User not found");
        }

        //var roles = await userManager.GetRolesAsync(user);

        return new AuthResponseDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            UserName = user.UserName!,
            AvatarUrl = user.AvatarUrl!,
            PhoneNumber = user.PhoneNumber!,
            //Roles = roles
        };
    }
}

