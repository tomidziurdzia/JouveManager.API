using JouveManager.Application.Features.Auths.Users.Commands.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JouveManager.WebApi.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var userGroup = app.MapGroup("/user");

        userGroup.MapPost("/login", async ([FromBody] LoginUserCommand request, IMediator mediator) =>
        {
            var result = await mediator.Send(request);
            return Results.Ok(result);
        });
    }
}