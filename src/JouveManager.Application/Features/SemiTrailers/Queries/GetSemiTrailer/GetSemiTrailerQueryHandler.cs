using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.SemiTrailer;
using JouveManager.Application.Exceptions;
using JouveManager.Domain;
using JouveManager.Domain.Repositories;

namespace JouveManager.Application.Features.SemiTrailers.Queries.GetSemiTrailer;

public class GetSemiTrailerQueryHandler(ISemiTrailerRepository semiTrailerRepository) : IQueryHandler<GetSemiTrailerQuery, SemiTrailerDto>
{
    public async Task<SemiTrailerDto> Handle(GetSemiTrailerQuery request, CancellationToken cancellationToken)
    {
        var semiTrailer = await semiTrailerRepository.Get(request.Id, cancellationToken);
        if (semiTrailer == null) throw new NotFoundException(nameof(SemiTrailer), request.Id);

        return new SemiTrailerDto
        {
            Id = semiTrailer.Id,
            LicensePlate = semiTrailer.LicensePlate,
            Brand = semiTrailer.Brand,
            Model = semiTrailer.Model,
            ImageUrl = semiTrailer.ImageUrl,
            Type = semiTrailer.Type
        };
    }
}
