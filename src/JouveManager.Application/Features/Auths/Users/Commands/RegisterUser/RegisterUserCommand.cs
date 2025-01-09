using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.User;

namespace JouveManager.Application.Features.Auths.Users.Commands.RegisterUser;

public class RegisterUserCommand : ICommand<AuthResponseDto>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }
    public required List<UserType> UserTypes { get; set; }
    public string? AvatarUrl { get; set; }
    public string? PhoneNumber { get; set; }
}