using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.SemiTrailer;

namespace JouveManager.Application.Features.SemiTrailers.Queries.GetSemiTrailer;

public class GetSemiTrailerQuery : IQuery<SemiTrailerDto>
{
    public Guid Id { get; set; }
    public GetSemiTrailerQuery(Guid id)
    {
        Id = id;
    }
}
