using FluentValidation;
using JouveManager.Domain.Enum;
using MediatR;

namespace JouveManager.Application.Features.TravelShipments.Commands.UpdateTravelShipmentStatus;

public class UpdateTravelShipmentStatusCommandValidator : AbstractValidator<UpdateTravelShipmentStatusCommand>
{
    public UpdateTravelShipmentStatusCommandValidator()
    {
        RuleFor(x => x.ShipmentId)
            .NotEmpty()
            .WithMessage("ShipmentId is required.");

        RuleFor(x => x.TravelId)
            .NotEmpty()
            .WithMessage("TravelId is required.");

        When(x => x.ShipmentStatus == ShipmentStatus.Delivered, () =>
        {
            RuleFor(x => x.DeliveryDate)
                .NotEmpty()
                .WithMessage("DeliveryDate is required when the shipment is delivered.");

            RuleFor(x => x.FailureReason)
                .Empty()
                .WithMessage("FailureReason must be empty when the shipment is delivered.");
        });

        When(x => x.ShipmentStatus == ShipmentStatus.Cancelled, () =>
        {
            RuleFor(x => x.FailureReason)
                .NotEmpty()
                .WithMessage("FailureReason is required when the shipment is not delivered.");

            RuleFor(x => x.DeliveryDate)
                .Empty()
                .WithMessage("DeliveryDate must be empty when the shipment is not delivered.");
        });
    }
}
