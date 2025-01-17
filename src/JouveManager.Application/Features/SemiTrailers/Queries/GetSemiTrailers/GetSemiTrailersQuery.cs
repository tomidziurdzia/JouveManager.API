using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.SemiTrailer;

namespace JouveManager.Application.Features.SemiTrailers.Queries.GetSemiTrailers;

public class GetSemiTrailersQuery : IQuery<IEnumerable<SemiTrailerDto>>
{

}
