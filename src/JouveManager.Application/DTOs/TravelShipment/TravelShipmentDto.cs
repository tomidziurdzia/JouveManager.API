namespace JouveManager.Application.DTOs.TravelShipment;

public class TravelShipmentDto
{
    public Guid ShipmentId { get; set; }
    public required string ShipmentDescription { get; set; }
    public Guid TravelId { get; set; }
    public DateTime AssignedDate { get; set; }
    public bool Delivered { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public required string FailureReason { get; set; }
}