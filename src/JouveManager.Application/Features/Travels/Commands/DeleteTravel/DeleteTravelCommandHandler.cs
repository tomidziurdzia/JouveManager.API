using JouveManager.Application.CQRS;
using JouveManager.Application.Exceptions;
using JouveManager.Domain.Repositories;
using MediatR;

namespace JouveManager.Application.Features.Travels.Commands.DeleteTravel;

public class DeleteTravelCommandHandler(ITravelRepository travelRepository) : ICommandHandler<DeleteTravelCommand, Unit>
{
    public async Task<Unit> Handle(DeleteTravelCommand request, CancellationToken cancellationToken)
    {
        var travel = await travelRepository.Get(request.Id, cancellationToken);
        if (travel is null) throw new NotFoundException("Travel", request.Id);

        await travelRepository.Delete(request.Id, cancellationToken);
        return Unit.Value;
    }
}