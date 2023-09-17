using Application.Features.Offers.Commands.CreateOffer;
using FluentValidation;

namespace Application.Features.Offers.Commands.CreateOffers;

public class CreateOfferCommandValidator : AbstractValidator<CreateOfferCommand>
{
    public CreateOfferCommandValidator()
    {
        RuleFor(p=>p.Description).NotNull();
        RuleFor(p=>p.Price).NotNull();
        RuleFor(p=>p.AppointmentDate).NotNull();
        RuleFor(p=>p.CorporateCustomerId).NotNull();
        RuleFor(p=>p.CrewId).NotNull();
        RuleFor(p=>p.TransportRequestId).NotNull();
    }
}