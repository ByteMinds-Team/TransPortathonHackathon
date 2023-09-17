using FluentValidation;

namespace Application.Features.Offers.Commands.AcceptOffer;

public class AcceptOfferCommandValidator : AbstractValidator<AcceptOfferCommand>
{
    public AcceptOfferCommandValidator()
    {
        RuleFor(p=>p.OfferId).NotNull();
        RuleFor(p=>p.UserId).NotNull();
    }
}