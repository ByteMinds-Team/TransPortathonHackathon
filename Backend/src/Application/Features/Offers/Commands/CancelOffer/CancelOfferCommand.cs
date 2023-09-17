using Application.Common.Behaviours.Authorization;
using Application.Features.Offers.Dtos;
using Application.Features.Offers.Rules;
using Application.Repositories.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Offers.Commands.CancelOffer;

public class CancelOfferCommand : IRequest<CanceledOfferDto>, ISecuredRequest
{
    public int OfferId { get; set; }
    public int UserId { get; set; }
    public string[] Roles => Array.Empty<string>();

    public class CancelOfferCommandHandler : IRequestHandler<CancelOfferCommand, CanceledOfferDto>
    {
        private readonly IOfferRepository _offerRepository;

        public CancelOfferCommandHandler(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<CanceledOfferDto> Handle(CancelOfferCommand request, CancellationToken cancellationToken)
        {
            var result = await _offerRepository
                .GetAsync(p => p.Id == request.OfferId &&
                    (p.TransportRequest.UserId == request.UserId || p.CorporateCustomerId == request.UserId),
                    include: p => p.Include(x => x.TransportRequest));

            OfferBusinessRules.checkNullData(result);
            OfferBusinessRules.checkCanceledField(result.IsCanceled);
            //TODO: Get rid of the redundant data.
            result.IsAccepted = false;
            result.IsCanceled = true;
            await _offerRepository.UpdateAsync(result);
            return new() { IsSuccess = true };
        }
    }
}