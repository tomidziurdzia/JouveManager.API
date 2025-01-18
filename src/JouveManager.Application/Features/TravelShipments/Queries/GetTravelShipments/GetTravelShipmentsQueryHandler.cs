using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.TravelShipment;
using JouveManager.Application.Exceptions;
using JouveManager.Domain.Repositories;

namespace JouveManager.Application.Features.TravelShipments.Queries.GetTravelShipments;

public class GetTravelShipmentsQueryHandler(ITravelShipmentRepository travelShipmentRepository) : IQueryHandler<GetTravelShipmentsQuery, TravelShipmentsDto>
{
    public async Task<TravelShipmentsDto> Handle(GetTravelShipmentsQuery request, CancellationToken cancellationToken)
    {
        var travelShipments = await travelShipmentRepository.GetTravelWithShipments(request.TravelId, cancellationToken);
        if (travelShipments is null) throw new NotFoundException(nameof(TravelShipment), request.TravelId);

        return new TravelShipmentsDto
        {
            Id = travelShipments.Id,
            Date = travelShipments.Date,
            DriverId = new Guid(travelShipments.DriverId),
            DriverName = $"{travelShipments.Driver.FirstName} {travelShipments.Driver.LastName}",
            AssistantId = travelShipments.AssistantId != null ? new Guid(travelShipments.AssistantId) : null,
            AssistantName = travelShipments.AssistantId != null ? $"{travelShipments.Assistant.FirstName} {travelShipments.Assistant.LastName}" : string.Empty,
            VehicleId = travelShipments.VehicleId,
            VehicleLicensePlate = travelShipments.Vehicle.LicensePlate,
            SemiTrailerId = travelShipments.SemiTrailerId,
            SemiTrailerLicensePlate = travelShipments.SemiTrailer?.LicensePlate ?? string.Empty,
            Shipments = travelShipments.TravelShipments.Select(ts => new ShipmentTravelsDto
            {
                ShipmentId = ts.ShipmentId,
                CustomerName = ts.Shipment.CustomerName,
                From = ts.Shipment.From,
                To = ts.Shipment.To,
                Description = ts.Shipment.Description,
                ScheduledDate = ts.Shipment.ScheduledDate,
                Delivered = ts.Delivered,
                DeliveryDate = ts.DeliveryDate,
                FailureReason = ts.FailureReason ?? string.Empty
            }).ToList()
        };
    }
}
