using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.Travel;

namespace JouveManager.Application.Features.Travels.Queries.GetTravel;

public class GetTravelQuery : IQuery<TravelDto>
{
    public Guid Id { get; set; }
    public GetTravelQuery(Guid id)
    {
        Id = id;
    }
}
