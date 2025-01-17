using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.Shipment;

namespace JouveManager.Application.Features.Shipments.Command.CreateShipment;

public class CreateShipmentCommand : ICommand<ShipmentDto>
{
    public required string CustomerName { get; set; }
    public required string From { get; set; }
    public required string To { get; set; }
    public required string Description { get; set; }
    public DateTime ScheduledDate { get; set; }
}
