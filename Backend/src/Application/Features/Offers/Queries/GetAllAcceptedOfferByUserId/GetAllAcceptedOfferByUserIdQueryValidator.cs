using FluentValidation;

namespace Application.Features.Offers.Queries.GetAllAcceptedOfferByUserId;

public class GetAllAcceptedOfferByUserIdQueryValidator : AbstractValidator<GetAllAcceptedOfferByUserIdQuery>
{
    public GetAllAcceptedOfferByUserIdQueryValidator()
    {
        RuleFor(p => p.UserId).NotNull();
    }
}