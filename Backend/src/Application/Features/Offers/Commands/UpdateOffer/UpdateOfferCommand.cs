using Application.Features.Offers.Dtos;
using Application.Features.Offers.Rules;
using Application.Repositories.EntityFramework;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Offers.Commands.UpdateOffer;

public class UpdateOfferCommand : IRequest<UpdatedOfferDto>
{
    public int OfferId { get; set; }
    public int UserId { get; set; }
    public bool IsCanceled { get; set; }
    public bool IsActived { get; set; }

    class UpdateOfferCommandHandler : IRequestHandler<UpdateOfferCommand,UpdatedOfferDto>
    {
        private readonly IMapper _mapper;
        private readonly IOfferRepository _offerRepository;

        public UpdateOfferCommandHandler(IMapper mapper, IOfferRepository offerRepository)
        {
            _mapper = mapper;
            _offerRepository = offerRepository;
        }

        public async Task<UpdatedOfferDto> Handle(UpdateOfferCommand request, CancellationToken cancellationToken)
        {
            var getData =await _offerRepository.GetAsync(p=>p.Id == request.OfferId && (p.CorporateCustomerId == request.UserId || p.TransportRequest.UserId == request.UserId),include:p=>p.Include(x=>x.TransportRequest));
            OfferBusinessRules.checkNullData(getData);
            var updatedData = await _offerRepository.UpdateAsync(_mapper.Map(request, getData));
            return _mapper.Map<UpdatedOfferDto>(updatedData);
        }
    }
}