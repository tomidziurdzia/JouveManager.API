using JouveManager.Application.DTOs.Shipment;
using JouveManager.Application.Features.Shipments.Command.CreateShipment;
using JouveManager.Application.Features.Shipments.Command.DeleteShipment;
using JouveManager.Application.Features.Shipments.Command.UpdateShipment;
using JouveManager.Application.Features.Shipments.Queries.GetShipment;
using JouveManager.Application.Features.Shipments.Queries.GetShipments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JouveManager.WebApi.Endpoints;

public static class ShipmentEndpoints
{
    public static IEndpointRouteBuilder RegisterShipmentEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var group = endpointRouteBuilder.MapGroup("/shipments");

        group.MapPost("/", async ([FromBody] CreateShipmentCommand request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(request, cancellationToken);
            return Results.Ok(result);
        })
        .WithName("CreateShipment")
        .Produces<ShipmentDto>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .RequireAuthorization();

        group.MapPut("/{id}", async ([FromRoute] Guid id, [FromBody] UpdateShipmentCommand request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            request.Id = id;
            var result = await mediator.Send(request, cancellationToken);
            return Results.Ok();
        })
        .WithName("UpdateShipment")
        .Produces<ShipmentDto>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .RequireAuthorization();

        group.MapDelete("/{id}", async ([FromRoute] Guid id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new DeleteShipmentCommand(id), cancellationToken);
            return Results.Ok();
        })
        .WithName("DeleteShipment")
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .RequireAuthorization();

        group.MapGet("/{id}", async ([FromRoute] Guid id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetShipmentQuery(id), cancellationToken);
            return Results.Ok(result);
        })
        .WithName("GetShipment")
        .Produces<ShipmentDto>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .RequireAuthorization();

        group.MapGet("/", async (IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetShipmentsQuery(), cancellationToken);
            return Results.Ok(result);
        })
        .WithName("GetShipments")
        .Produces<IEnumerable<ShipmentDto>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .RequireAuthorization();

        return endpointRouteBuilder;
    }
}
