using JouveManager.Application.CQRS;
using JouveManager.Application.Exceptions;
using JouveManager.Domain;
using JouveManager.Domain.Repositories;
using MediatR;

namespace JouveManager.Application.Features.SemiTrailers.Commands.DeleteSemiTrailer;

public class DeleteSemiTrailerCommandHandler(ISemiTrailerRepository semiTrailerRepository) : ICommandHandler<DeleteSemiTrailerCommand, Unit>
{
    public async Task<Unit> Handle(DeleteSemiTrailerCommand request, CancellationToken cancellationToken)
    {
        var semiTrailer = await semiTrailerRepository.Get(request.Id, cancellationToken);
        if (semiTrailer is null) throw new NotFoundException(nameof(SemiTrailer), request.Id);

        await semiTrailerRepository.Delete(semiTrailer.Id, cancellationToken);
        return Unit.Value;
    }
}
