namespace JouveManager.Application.DTOs.Shipment;

public class CreateShipmentDto
{
    public required string CustomerName { get; set; }
    public required string From { get; set; }
    public required string To { get; set; }
    public required string Description { get; set; }
    public DateTime ScheduledDate { get; set; }
}
