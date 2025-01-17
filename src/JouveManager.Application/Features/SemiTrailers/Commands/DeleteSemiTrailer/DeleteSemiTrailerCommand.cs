
using JouveManager.Application.CQRS;
using MediatR;

namespace JouveManager.Application.Features.SemiTrailers.Commands.DeleteSemiTrailer;

public class DeleteSemiTrailerCommand(Guid id) : ICommand<Unit>
{
    public Guid Id { get; set; } = id;
}
