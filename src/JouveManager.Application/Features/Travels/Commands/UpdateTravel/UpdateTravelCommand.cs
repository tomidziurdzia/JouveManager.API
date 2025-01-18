using JouveManager.Application.CQRS;
using MediatR;

namespace JouveManager.Application.Features.Travels.Commands.UpdateTravel;

public class UpdateTravelCommand : ICommand<Unit>
{
    public Guid Id { get; set; }
    public DateTime? Date { get; set; }
    public string? DriverId { get; set; }
    public string? AssistantId { get; set; }
    public Guid? VehicleId { get; set; }
    public Guid? SemiTrailerId { get; set; }
}