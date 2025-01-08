using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.User;

namespace JouveManager.Application.Features.Auths.Users.Commands.LoginUser;

public class LoginUserCommand : ICommand<AuthResponseDto>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
