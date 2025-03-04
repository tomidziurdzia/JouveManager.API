using JouveManager.Domain.Enum;

namespace JouveManager.Application.DTOs.TravelShipmentHistory;

public class TravelShipmentHistoryDto
{
    public Guid Id { get; set; }
    public ShipmentStatus OldStatus { get; set; }
    public ShipmentStatus NewStatus { get; set; }
    public string? Comments { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}
