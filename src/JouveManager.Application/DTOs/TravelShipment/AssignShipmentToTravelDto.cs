namespace JouveManager.Application.DTOs.TravelShipment;

public class AssignShipmentToTravelDto
{
    public required Guid ShipmentId { get; set; }
    public required Guid TravelId { get; set; }
}
