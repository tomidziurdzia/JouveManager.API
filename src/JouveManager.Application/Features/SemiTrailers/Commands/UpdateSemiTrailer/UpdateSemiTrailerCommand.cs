using JouveManager.Application.CQRS;
using JouveManager.Domain.Enum;
using MediatR;

namespace JouveManager.Application.Features.SemiTrailers.Commands.UpdateSemiTrailer;

public class UpdateSemiTrailerCommand : ICommand<Unit>
{
    public Guid Id { get; set; }
    public string? LicensePlate { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public string? ImageUrl { get; set; }
    public TypeSemiTrailer? Type { get; set; }
}
