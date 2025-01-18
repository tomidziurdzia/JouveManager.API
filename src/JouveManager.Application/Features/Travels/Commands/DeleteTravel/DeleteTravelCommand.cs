using JouveManager.Application.CQRS;
using MediatR;

namespace JouveManager.Application.Features.Travels.Commands.DeleteTravel;

public class DeleteTravelCommand(Guid id) : ICommand<Unit>
{
    public Guid Id { get; set; } = id;
}