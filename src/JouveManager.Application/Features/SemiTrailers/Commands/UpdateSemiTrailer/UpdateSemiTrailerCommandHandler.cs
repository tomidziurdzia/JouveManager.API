using JouveManager.Application.CQRS;
using JouveManager.Application.Exceptions;
using JouveManager.Domain;
using JouveManager.Domain.Repositories;
using MediatR;

namespace JouveManager.Application.Features.SemiTrailers.Commands.UpdateSemiTrailer;

public class UpdateSemiTrailerCommandHandler(ISemiTrailerRepository semiTrailerRepository) : ICommandHandler<UpdateSemiTrailerCommand, Unit>
{
    public async Task<Unit> Handle(UpdateSemiTrailerCommand request, CancellationToken cancellationToken)
    {
        var semiTrailer = await semiTrailerRepository.Get(request.Id, cancellationToken);
        if (semiTrailer is null) throw new NotFoundException(nameof(SemiTrailer), request.Id);

        semiTrailer.LicensePlate = request.LicensePlate ?? semiTrailer.LicensePlate;
        semiTrailer.Brand = request.Brand ?? semiTrailer.Brand;
        semiTrailer.Model = request.Model ?? semiTrailer.Model;
        semiTrailer.ImageUrl = request.ImageUrl ?? semiTrailer.ImageUrl;
        semiTrailer.Type = request.Type ?? semiTrailer.Type;

        await semiTrailerRepository.Update(semiTrailer, cancellationToken);
        return Unit.Value;
    }
}
