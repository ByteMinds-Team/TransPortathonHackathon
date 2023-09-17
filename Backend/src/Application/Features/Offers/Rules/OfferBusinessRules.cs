using Application.Common.Exceptions;
using Domain;

namespace Application.Features.Offers.Rules;

public static class OfferBusinessRules
{
    public static void checkNullData(Offer offer)
    {
        if (offer is null)
        {
            throw new BusinessException("offer is not found");
        }
    }
    
    public static void checkIfAlreadyAccept(bool isAccepted)
    {
        if (isAccepted)
        {
            throw new BusinessException("offer already accepted ");
        }
    }
    
    public static void checkCanceledField(bool isCanceled)
    {
        if (isCanceled)
        {
            throw new BusinessException("offer already canceled ");
        }
    }
}