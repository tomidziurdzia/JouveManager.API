using FluentValidation;

namespace JouveManager.Application.Features.Travels.Commands.UpdateTravel;

public class UpdateTravelCommandValidator : AbstractValidator<UpdateTravelCommand>
{
    public UpdateTravelCommandValidator()
    {
        RuleFor(x => x.Date).NotEmpty();
        RuleFor(x => x.DriverId).NotEmpty();
        RuleFor(x => x.AssistantId).NotEmpty();
        RuleFor(x => x.VehicleId).NotEmpty();
        RuleFor(x => x.SemiTrailerId).NotEmpty();
    }
}