using JouveManager.Domain.Enum;

namespace JouveManager.Application.DTOs.Shipment;

public class ShipmentDto
{
    public Guid Id { get; set; }
    public required string CustomerName { get; set; }
    public required string From { get; set; }
    public required string To { get; set; }
    public required string Description { get; set; }
    public DateTime ScheduledDate { get; set; }
    public bool IsAssigned { get; set; }
}
