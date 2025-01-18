using FluentValidation;

namespace JouveManager.Application.Features.Travels.Commands.CreateTravel;

public class CreateTravelCommandValidator : AbstractValidator<CreateTravelCommand>
{
    public CreateTravelCommandValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required");

        RuleFor(x => x.DriverId)
            .NotEmpty().WithMessage("Driver is required");

        RuleFor(x => x.VehicleId)
            .NotEmpty().WithMessage("Vehicle is required");

        When(x => !string.IsNullOrEmpty(x.AssistantId), () =>
        {
            RuleFor(x => x.AssistantId)
                .NotEmpty().WithMessage("Assistant ID cannot be empty when provided");
        });

        When(x => x.SemiTrailerId.HasValue && x.SemiTrailerId != Guid.Empty, () =>
        {
            RuleFor(x => x.SemiTrailerId)
                .Must(id => id != Guid.Empty)
                .WithMessage("SemiTrailer ID cannot be empty GUID when provided");
        });
    }
}
