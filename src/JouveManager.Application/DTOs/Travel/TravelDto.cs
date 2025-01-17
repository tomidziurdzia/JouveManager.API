namespace JouveManager.Application.DTOs.Travel;

public class TravelDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public Guid DriverId { get; set; }
    public required string DriverName { get; set; }
    public Guid? AssistantId { get; set; }
    public required string AssistantName { get; set; }
    public Guid VehicleId { get; set; }
    public required string VehicleLicensePlate { get; set; }
    public Guid? SemiTrailerId { get; set; }
    public required string SemiTrailerLicensePlate { get; set; }
}
