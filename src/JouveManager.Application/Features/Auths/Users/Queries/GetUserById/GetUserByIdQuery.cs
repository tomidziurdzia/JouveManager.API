using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.User;

namespace JouveManager.Application.Features.Auths.Users.Queries.GetUserById;

public class GetUserByIdQuery(string userId) : IQuery<AuthResponseDto>
{
    public string Id { get; set; } = userId ?? throw new ArgumentNullException(nameof(userId));
}
