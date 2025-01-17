using JouveManager.Application.CQRS;
using MediatR;

namespace JouveManager.Application.Features.Shipments.Command.UpdateShipment;

public class UpdateShipmentCommand : ICommand<Unit>
{
    public Guid Id { get; set; }
    public string? CustomerName { get; set; }
    public string? From { get; set; }
    public string? To { get; set; }
    public string? Description { get; set; }
    public DateTime ScheduledDate { get; set; }
}
