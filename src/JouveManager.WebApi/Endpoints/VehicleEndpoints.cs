using JouveManager.Application.DTOs.Vehicle;
using JouveManager.Application.Features.Vehicles.Commands.CreateVehicle;
using JouveManager.Application.Features.Vehicles.Commands.DeleteVehicle;
using JouveManager.Application.Features.Vehicles.Commands.UpdateVehicle;
using JouveManager.Application.Features.Vehicles.Queries.GetVehicle;
using JouveManager.Application.Features.Vehicles.Queries.GetVehicles;
using JouveManager.Application.Models.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JouveManager.WebApi.Endpoints;

public static class VehicleEndpoints
{
    public static IEndpointRouteBuilder RegisterVehicleEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var group = endpointRouteBuilder.MapGroup("/vehicles");

        group.MapPost("/", async ([FromBody] CreateVehicleCommand request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(request, cancellationToken);
            return Results.Ok(result);
        })
        .WithName("CreateVehicle")
        .Produces<VehicleDto>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .RequireAuthorization(policy => policy.RequireRole(Role.Owner, Role.Manager, Role.Administrative));

        group.MapPut("/{id}", async ([FromRoute] Guid id, [FromBody] UpdateVehicleCommand request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            request.Id = id;
            var result = await mediator.Send(request, cancellationToken);
            return Results.Ok();
        })
        .WithName("UpdateVehicle")
        .Produces<Unit>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .RequireAuthorization(policy => policy.RequireRole(Role.Owner, Role.Manager, Role.Administrative));

        group.MapDelete("/{id}", async ([FromRoute] Guid id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new DeleteVehicleCommand(id), cancellationToken);
            return Results.Ok();
        })
        .WithName("DeleteVehicle")
        .Produces<Unit>(StatusCodes.Status200OK)
        .RequireAuthorization(policy => policy.RequireRole(Role.Owner, Role.Manager));

        group.MapGet("/{id}", async ([FromRoute] Guid id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetVehicleQuery(id), cancellationToken);
            return Results.Ok(result);
        })
        .WithName("GetVehicle")
        .Produces<VehicleDto>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .RequireAuthorization();

        group.MapGet("/", async (IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetVehiclesQuery(), cancellationToken);
            return Results.Ok(result);
        })
        .WithName("GetVehicles")
        .Produces<IEnumerable<VehicleDto>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .RequireAuthorization();

        return endpointRouteBuilder;
    }
}
