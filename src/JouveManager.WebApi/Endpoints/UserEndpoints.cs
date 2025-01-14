using JouveManager.Application.DTOs.User;
using JouveManager.Application.Features.Auths.Users.Commands.LoginUser;
using JouveManager.Application.Features.Auths.Users.Commands.RegisterUser;
using JouveManager.Application.Features.Auths.Users.Queries.GetUserById;
using JouveManager.Application.Features.Auths.Users.Queries.GetUserByToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JouveManager.WebApi.Endpoints;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder RegisterUserEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var group = endpointRouteBuilder.MapGroup("/users");

        group.MapPost("/login", async ([FromBody] LoginUserCommand request, IMediator mediator, CancellationToken cancellationToken) =>
            {
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

        group.MapGet("/{id}", async (string id, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(new GetUserByIdQuery(id), cancellationToken);
                return Results.Ok(result);
            })
            .WithName("GetUserById")
            .Produces<AuthResponseDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound);
        
        group.MapGet("/me", async (IMediator mediator, CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(new GetUserByToken(), cancellationToken);
                return Results.Ok(result);
            })
            .WithName("GetUserByToken")
            .Produces<AuthResponseDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .RequireAuthorization();

        return endpointRouteBuilder;
    }
}