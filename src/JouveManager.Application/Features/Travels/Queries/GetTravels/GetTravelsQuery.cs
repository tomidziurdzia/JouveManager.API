using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.Travel;

namespace JouveManager.Application.Features.Travels.Queries.GetTravels;

public class GetTravelsQuery : IQuery<IEnumerable<TravelDto>>
{

}
