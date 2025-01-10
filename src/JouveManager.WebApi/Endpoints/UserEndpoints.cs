using JouveManager.Application.DTOs.User;
using JouveManager.Application.Features.Auths.Users.Commands.LoginUser;
using JouveManager.Application.Features.Auths.Users.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder RegisterUserEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var group = endpointRouteBuilder.MapGroup("/users");

        group.MapPost("/login", async ([FromBody] LoginUserCommand request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            Console.WriteLine(request);
            var result = await mediator.Send(request, cancellationToken);
            return Results.Ok(result);
        })
        .WithName("LoginUser")
            .Produces<AuthResponseDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapPost("/register", async ([FromBody] RegisterUserCommand request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(request, cancellationToken);
            return Results.Ok(result);
        })
        .WithName("RegisterUser")
            .Produces<AuthResponseDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);

        return endpointRouteBuilder;
    }
}
