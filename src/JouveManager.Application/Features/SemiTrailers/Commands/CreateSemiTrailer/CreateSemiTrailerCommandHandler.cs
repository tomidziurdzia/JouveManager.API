using JouveManager.Application.CQRS;
using JouveManager.Application.DTOs.SemiTrailer;
using JouveManager.Domain;
using JouveManager.Domain.Repositories;

namespace JouveManager.Application.Features.SemiTrailers.Commands.CreateSemiTrailer;

public class CreateSemiTrailerCommandHandler(ISemiTrailerRepository semiTrailerRepository) : ICommandHandler<CreateSemiTrailerCommand, SemiTrailerDto>
{
    public async Task<SemiTrailerDto> Handle(CreateSemiTrailerCommand request, CancellationToken cancellationToken)
    {
        var semiTrailer = new SemiTrailer
        {
            LicensePlate = request.LicensePlate,
            Brand = request.Brand,
            Model = request.Model,
            ImageUrl = request.ImageUrl,
            Type = request.Type,
        };

        await semiTrailerRepository.Create(semiTrailer, cancellationToken);

        return new SemiTrailerDto
        {
            Id = semiTrailer.Id,
            LicensePlate = semiTrailer.LicensePlate,
            Brand = semiTrailer.Brand,
            Model = semiTrailer.Model,
            ImageUrl = semiTrailer.ImageUrl,
            Type = semiTrailer.Type,
        };
    }
}
