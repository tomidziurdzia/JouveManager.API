using JouveManager.Application.DTOs.User;
using JouveManager.Application.Features.Auths.Users.Commands.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/login", async ([FromBody] LoginUserCommand request, IMediator mediator) =>
        {

            Console.WriteLine(request);
            var result = await mediator.Send(request);
            return Results.Ok(result);
        })
        .WithName("LoginUser")
            .Produces<AuthResponseDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Login user")
            .WithDescription("This endpoint returns a user.");
    }
}
