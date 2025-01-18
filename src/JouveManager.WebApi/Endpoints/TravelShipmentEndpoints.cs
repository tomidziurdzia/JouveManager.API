using System;
using JouveManager.Application.DTOs.TravelShipment;
using JouveManager.Application.Features.TravelShipments.Commands.AssignTravelShipment;
using JouveManager.Application.Features.TravelShipments.Commands.UnassignShipmentFromTravel;
using JouveManager.Application.Features.TravelShipments.Commands.UpdateTravelShipmentStatus;
using JouveManager.Application.Features.TravelShipments.Queries.GetTravelShipments;
using JouveManager.Application.Features.TravelShipments.Queries.GetTravelsShipments;
using JouveManager.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JouveManager.WebApi.Endpoints;

public static class TravelShipmentEndpoints
{
    public static IEndpointRouteBuilder RegisterTravelShipmentEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var group = endpointRouteBuilder.MapGroup("/travel-shipments");

        group.MapPost("/assign", async ([FromBody] AssignShipmentToTravelCommand request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(request, cancellationToken);
            return Results.NoContent();
        })
        .WithName("AssignShipmentToTravel")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .RequireAuthorization();

        group.MapPost("/unassign", async ([FromBody] UnassignShipmentFromTravelCommand request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(request, cancellationToken);
            return Results.NoContent();
        })
        .WithName("UnassignShipmentFromTravel")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .RequireAuthorization();

        group.MapPut("/status", async ([FromBody] UpdateTravelShipmentStatusCommand request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(request, cancellationToken);
            return Results.NoContent();
        })
        .WithName("UpdateTravelShipmentStatus")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .RequireAuthorization();

        group.MapGet("/{id}", async ([FromRoute] Guid id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetTravelShipmentsQuery(id), cancellationToken);
            return Results.Ok(result);
        })
        .WithName("GetTravelShipments")
        .Produces<TravelShipmentsDto>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .RequireAuthorization();

        group.MapGet("/", async (IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetTravelsShipmentsQuery(), cancellationToken);
            return Results.Ok(result);
        })
        .WithName("GetTravelsShipments")
        .Produces<IEnumerable<TravelShipmentsDto>>(StatusCodes.Status200OK)
        .RequireAuthorization();

        return endpointRouteBuilder;
    }
}
