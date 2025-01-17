namespace JouveManager.Application.DTOs.Travel;

public class UpdateTravelDto
{
    public required DateTime Date { get; set; }
    public required Guid DriverId { get; set; }
    public Guid? AssistantId { get; set; }
    public required Guid VehicleId { get; set; }
    public Guid? SemiTrailerId { get; set; }
}
