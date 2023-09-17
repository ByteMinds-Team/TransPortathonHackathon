using Application.Common.Exceptions;
using Domain;

namespace Application.Features.Reviews.Rules;

public static class ReviewsBusinessRules
{
    public static void ThrowErrorIfOfferIsNull(Offer offer)
    {
        if (offer is null)
            throw new BusinessException("Offer not found");
    }
    
    public static void ThrowErrorIfOfferDate(Offer offer)
    {
        if (offer.AppointmentDate >= DateTime.Now.AddDays(1))
            throw new BusinessException("You can comment 1 day after the move");
    }
}