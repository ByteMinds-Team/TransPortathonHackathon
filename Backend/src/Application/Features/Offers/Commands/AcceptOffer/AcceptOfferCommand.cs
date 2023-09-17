using Application.Common.Behaviours.Authorization;
using Application.Features.Offers.Dtos;
using Application.Features.Offers.Rules;
using Application.Repositories.EntityFramework;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Offers.Commands.AcceptOffer;

public class AcceptOfferCommand : IRequest<AcceptedOfferDto> , ISecuredRequest
{
    public int OfferId { get; set; }
    public int UserId { get; set; }


    public string[] Roles => Array.Empty<string>();
    
    public class AcceptOfferCommandHandler : IRequestHandler<AcceptOfferCommand,AcceptedOfferDto>
    {
        private readonly IMapper _mapper;
        private readonly IOfferRepository _offerRepository;

        public AcceptOfferCommandHandler(IMapper mapper, IOfferRepository offerRepository)
        {
            _mapper = mapper;
            _offerRepository = offerRepository;
        }

        public async Task<AcceptedOfferDto> Handle(AcceptOfferCommand request, CancellationToken cancellationToken)
        {
            var result = await _offerRepository.GetAsync(p=>p.Id == request.OfferId && p.TransportRequest.UserId == request.UserId,include: p=> p.Include(x=>x.TransportRequest));
            //TODO! : if result.IsAccepted is true expect
            OfferBusinessRules.checkNullData(result);
            OfferBusinessRules.checkIfAlreadyAccept(result.IsAccepted);
            result.IsAccepted = true;
            await _offerRepository.UpdateAsync(result);
            return new() {IsSuccess = true};
        }
    }
}