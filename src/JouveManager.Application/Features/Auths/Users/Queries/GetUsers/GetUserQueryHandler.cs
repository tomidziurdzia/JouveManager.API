using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.User;
using JouveManager.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JouveManager.Application.Features.Auths.Users.Queries.GetUsers;

public class GetUserQueryHandler(
    UserManager<User> userManager) : IQueryHandler<GetUserQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var users = await userManager.Users.ToListAsync(cancellationToken);
        var userDtos = new List<UserDto>();

        foreach (var user in users)
        {
            var roles = await userManager.GetRolesAsync(user);
            userDtos.Add(new UserDto
            {
                Id = user.Id,
                FullName = user.FirstName + " " + user.LastName,
                Email = user.Email!,
                AvatarUrl = user.AvatarUrl,
                Roles = roles
            });
        }

        return userDtos;
    }
}