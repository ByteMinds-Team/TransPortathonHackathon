using Application.Features.Offers.Commands.AcceptOffer;
using FluentValidation;

namespace Application.Features.Offers.Commands.CancelOffer;

public class CancelOfferCommandValidator: AbstractValidator<AcceptOfferCommand>
{
    public CancelOfferCommandValidator()
    {
        RuleFor(p=>p.OfferId).NotNull();
        RuleFor(p=>p.UserId).NotNull();
    }
}