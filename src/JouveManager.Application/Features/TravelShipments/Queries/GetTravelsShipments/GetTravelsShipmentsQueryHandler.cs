
using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.TravelShipment;
using JouveManager.Domain.Repositories;

namespace JouveManager.Application.Features.TravelShipments.Queries.GetTravelsShipments;

public class GetTravelsShipmentsQueryHandler(ITravelShipmentRepository travelShipmentRepository) : IQueryHandler<GetTravelsShipmentsQuery, IEnumerable<TravelShipmentsDto>>
{
    public async Task<IEnumerable<TravelShipmentsDto>> Handle(GetTravelsShipmentsQuery request, CancellationToken cancellationToken)
    {
        var travelShipments = await travelShipmentRepository.GetTravelsWithShipments(cancellationToken);

        return travelShipments.Select(travel => new TravelShipmentsDto
        {
            Id = travel.Id,
            Date = travel.Date,
            DriverId = new Guid(travel.DriverId),
            DriverName = $"{travel.Driver.FirstName} {travel.Driver.LastName}",
            AssistantId = travel.AssistantId != null ? new Guid(travel.AssistantId) : null,
            AssistantName = travel.AssistantId != null ? $"{travel.Assistant.FirstName} {travel.Assistant.LastName}" : string.Empty,
            VehicleId = travel.VehicleId,
            VehicleLicensePlate = travel.Vehicle.LicensePlate,
            SemiTrailerId = travel.SemiTrailerId,
            SemiTrailerLicensePlate = travel.SemiTrailer?.LicensePlate ?? string.Empty,
            Shipments = travel.TravelShipments.Select(ts => new ShipmentTravelsDto
            {
                ShipmentId = ts.ShipmentId,
                CustomerName = ts.Shipment.CustomerName,
                From = ts.Shipment.From,
                To = ts.Shipment.To,
                Description = ts.Shipment.Description,
                ScheduledDate = ts.Shipment.ScheduledDate,
                ShipmentStatus = ts.ShipmentStatus,
                DeliveryDate = ts.DeliveryDate,
                FailureReason = ts.FailureReason ?? string.Empty
            }).ToList()
        }).ToList();
    }
}