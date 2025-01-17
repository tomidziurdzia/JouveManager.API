using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.SemiTrailer;
using JouveManager.Domain.Repositories;

namespace JouveManager.Application.Features.SemiTrailers.Queries.GetSemiTrailers;

public class GetSemiTrailersQueryHandler(ISemiTrailerRepository semiTrailerRepository) : IQueryHandler<GetSemiTrailersQuery, IEnumerable<SemiTrailerDto>>
{
    public async Task<IEnumerable<SemiTrailerDto>> Handle(GetSemiTrailersQuery request, CancellationToken cancellationToken)
    {
        var semiTrailers = await semiTrailerRepository.GetAll(cancellationToken);

        return semiTrailers.Select(
            v => new SemiTrailerDto
            {
                Id = v.Id,
                LicensePlate = v.LicensePlate,
                Brand = v.Brand,
                Model = v.Model,
                ImageUrl = v.ImageUrl,
                Type = v.Type
            });
    }
}
