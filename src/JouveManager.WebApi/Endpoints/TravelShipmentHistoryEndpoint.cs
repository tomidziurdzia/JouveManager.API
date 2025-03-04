using JouveManager.Application.DTOs.TravelShipmentHistory;
using JouveManager.Application.Features.TravelShipmentHistory.Queries.GetTravelShipmentHistory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JouveManager.WebApi.Endpoints;

public static class TravelShipmentHistoryEndpoint
{
    public static IEndpointRouteBuilder RegisterTravelShipmentHistoryEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var group = endpointRouteBuilder.MapGroup("/travel-shipment-history");

        group.MapGet("/{id}", async ([FromRoute] Guid id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetTravelShipmentHistoryQuery(id), cancellationToken);
            return Results.Ok(result);
        })
        .WithName("GetTravelShipmentHistory")
        .Produces<IEnumerable<TravelShipmentHistoryDto>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .RequireAuthorization();

        return endpointRouteBuilder;
    }
}
