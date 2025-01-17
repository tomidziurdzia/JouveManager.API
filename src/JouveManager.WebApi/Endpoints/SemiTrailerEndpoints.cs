using JouveManager.Application.DTOs.SemiTrailer;
using JouveManager.Application.Features.SemiTrailers.Commands.CreateSemiTrailer;
using JouveManager.Application.Features.SemiTrailers.Commands.DeleteSemiTrailer;
using JouveManager.Application.Features.SemiTrailers.Commands.UpdateSemiTrailer;
using JouveManager.Application.Features.SemiTrailers.Queries.GetSemiTrailer;
using JouveManager.Application.Features.SemiTrailers.Queries.GetSemiTrailers;
using JouveManager.Application.Models.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JouveManager.WebApi.Endpoints;

public static class SemiTrailerEndpoints
{
    public static IEndpointRouteBuilder RegisterSemiTrailerEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var group = endpointRouteBuilder.MapGroup("/semi-trailers");

        group.MapPost("/", async ([FromBody] CreateSemiTrailerCommand request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(request, cancellationToken);
            return Results.Ok(result);
        })
        .WithName("CreateSemiTrailer")
        .Produces<SemiTrailerDto>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .RequireAuthorization(policy => policy.RequireRole(Role.Owner, Role.Manager, Role.Administrative));

        group.MapPut("/{id}", async ([FromRoute] Guid id, [FromBody] UpdateSemiTrailerCommand request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            request.Id = id;
            var result = await mediator.Send(request, cancellationToken);
            return Results.Ok();
        })
        .WithName("UpdateSemiTrailer")
        .Produces<Unit>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .RequireAuthorization(policy => policy.RequireRole(Role.Owner, Role.Manager, Role.Administrative));

        group.MapDelete("/{id}", async ([FromRoute] Guid id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new DeleteSemiTrailerCommand(id), cancellationToken);
            return Results.Ok();
        })
        .WithName("DeleteSemiTrailer")
        .Produces<Unit>(StatusCodes.Status200OK)
        .RequireAuthorization(policy => policy.RequireRole(Role.Owner, Role.Manager));

        group.MapGet("/{id}", async ([FromRoute] Guid id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetSemiTrailerQuery(id), cancellationToken);
            return Results.Ok(result);
        })
        .WithName("GetSemiTrailer")
        .Produces<SemiTrailerDto>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .RequireAuthorization();

        group.MapGet("/", async (IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetSemiTrailersQuery(), cancellationToken);
            return Results.Ok(result);
        })
        .WithName("GetSemiTrailers")
        .Produces<IEnumerable<SemiTrailerDto>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .RequireAuthorization();

        return endpointRouteBuilder;
    }
}
