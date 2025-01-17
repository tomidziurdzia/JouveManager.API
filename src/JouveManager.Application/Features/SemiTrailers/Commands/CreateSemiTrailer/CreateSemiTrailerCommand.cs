using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.SemiTrailer;
using JouveManager.Domain.Enum;

namespace JouveManager.Application.Features.SemiTrailers.Commands.CreateSemiTrailer;

public class CreateSemiTrailerCommand : ICommand<SemiTrailerDto>
{
    public required string LicensePlate { get; set; }
    public required string Brand { get; set; }
    public required string Model { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public required TypeSemiTrailer Type { get; set; }
}
