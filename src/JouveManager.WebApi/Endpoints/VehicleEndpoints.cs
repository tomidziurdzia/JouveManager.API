using JouveManager.Application.DTOs.Vehicle;
using JouveManager.Application.Features.Vehicles.Commands.CreateVehicle;
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
        .RequireAuthorization();

        return endpointRouteBuilder;
    }
}
