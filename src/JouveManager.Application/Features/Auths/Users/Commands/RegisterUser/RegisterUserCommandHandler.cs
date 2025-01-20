using JouveManager.Application.Contracts.Identity;
using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.User;
using JouveManager.Application.Exceptions;
using JouveManager.Domain;
using Microsoft.AspNetCore.Identity;

namespace JouveManager.Application.Features.Auths.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler(UserManager<User> userManager, IAuthService authService)
    : ICommandHandler<RegisterUserCommand, AuthResponseDto>
{
    public async Task<AuthResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var userExists = await userManager.FindByEmailAsync(request.Email);
        if (userExists is not null)
        {
            throw new BadRequestException("User already exists");
        }

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.Email,
            AvatarUrl = request.AvatarUrl,
            PhoneNumber = request.PhoneNumber
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            throw new Exception("Failed to create user");
        }

        await userManager.AddToRoleAsync(user, request.Role);

        var roles = await userManager.GetRolesAsync(user);

        var token = authService.CreateToken(user, roles);

        return new AuthResponseDto()
        {
            Id = user.Id,
            FirstName = user.FirstName!,
            LastName = user.LastName!,
            Email = user.Email!,
            UserName = user.UserName!,
            AvatarUrl = user.AvatarUrl!,
            Token = token,
            Roles = roles.ToList()
        };
    }
}
