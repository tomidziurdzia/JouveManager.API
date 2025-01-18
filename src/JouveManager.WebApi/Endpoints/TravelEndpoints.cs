using JouveManager.Application.Features.Travels.Commands.CreateTravel;
using JouveManager.Application.DTOs.Travel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using JouveManager.Application.Features.Travels.Commands.UpdateTravel;
using JouveManager.Application.Features.Travels.Commands.DeleteTravel;
using JouveManager.Application.Features.Travels.Queries.GetTravels;
using JouveManager.Application.Features.Travels.Queries.GetTravel;

namespace JouveManager.WebApi.Endpoints;

public static class TravelEndpoints
{
    public static IEndpointRouteBuilder RegisterTravelEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var group = endpointRouteBuilder.MapGroup("/travels");

        group.MapPost("/", async ([FromBody] CreateTravelCommand request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(request, cancellationToken);
            return Results.Ok(result);
        })
        .WithName("CreateTravel")
        .Produces<TravelDto>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .RequireAuthorization();

        group.MapPut("/{id}", async ([FromRoute] Guid id, [FromBody] UpdateTravelCommand request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            request.Id = id;
            await mediator.Send(request, cancellationToken);
            return Results.NoContent();
        })
        .WithName("UpdateTravel")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .RequireAuthorization();

        group.MapDelete("/{id}", async ([FromRoute] Guid id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(new DeleteTravelCommand(id), cancellationToken);
            return Results.NoContent();
        })
        .WithName("DeleteTravel")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .RequireAuthorization();

        group.MapGet("/", async (IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetTravelsQuery(), cancellationToken);
            return Results.Ok(result);
        })
        .WithName("GetTravels")
        .Produces<IEnumerable<TravelDto>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .RequireAuthorization();

        group.MapGet("/{id}", async ([FromRoute] Guid id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetTravelQuery(id), cancellationToken);
            return Results.Ok(result);
        })
        .WithName("GetTravel")
        .Produces<TravelDto>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .RequireAuthorization();

        return endpointRouteBuilder;
    }
}
