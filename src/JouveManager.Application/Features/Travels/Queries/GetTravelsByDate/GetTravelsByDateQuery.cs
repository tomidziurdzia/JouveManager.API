using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.Travel;

namespace JouveManager.Application.Features.Travels.Queries.GetTravelsByDate;

public class GetTravelsByDateQuery : IQuery<IEnumerable<TravelDto>>
{
    public DateTime Date { get; }

    public GetTravelsByDateQuery(DateTime date)
    {
        Date = date;
    }
}
