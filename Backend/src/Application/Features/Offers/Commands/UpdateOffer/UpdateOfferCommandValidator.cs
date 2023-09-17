using FluentValidation;

namespace Application.Features.Offers.Commands.UpdateOffer;

public class UpdateOfferCommandValidator : AbstractValidator<UpdateOfferCommand>
{
    public UpdateOfferCommandValidator()
    {
        RuleFor(p=> p.UserId).NotNull();
        RuleFor(p=> p.OfferId).NotNull();
    }
}