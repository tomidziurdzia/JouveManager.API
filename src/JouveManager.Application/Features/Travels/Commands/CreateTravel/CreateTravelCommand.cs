using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.Travel;

namespace JouveManager.Application.Features.Travels.Commands.CreateTravel;

public class CreateTravelCommand : ICommand<TravelDto>
{
    public DateTime Date { get; set; }
    public required string DriverId { get; set; }
    public string? AssistantId { get; set; }
    public required Guid VehicleId { get; set; }
    public Guid? SemiTrailerId { get; set; }
}
