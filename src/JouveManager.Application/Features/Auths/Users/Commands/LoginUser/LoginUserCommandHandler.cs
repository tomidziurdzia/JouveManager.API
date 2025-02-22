using JouveManager.Application.Contracts.Identity;
using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.User;
using JouveManager.Application.Exceptions;
using JouveManager.Domain;
using Microsoft.AspNetCore.Identity;

namespace JouveManager.Application.Features.Auths.Users.Commands.LoginUser;

public class LoginUserCommandHandler(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    IAuthService authService) : ICommandHandler<LoginUserCommand, AuthResponseDto>
{
    public async Task<AuthResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.Email!);
        }

        var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (!result.Succeeded)
        {
            throw new Exception("Email not found and/or password incorrect");
        }

        var roles = await userManager.GetRolesAsync(user);

        var token = authService.CreateToken(user, roles);

        var authResponse = new AuthResponseDto()
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

        return authResponse;
    }
}
