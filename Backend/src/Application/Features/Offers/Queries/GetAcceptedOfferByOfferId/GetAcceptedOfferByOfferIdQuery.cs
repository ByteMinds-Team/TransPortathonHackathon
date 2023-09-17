using Application.Common.Behaviours.Authorization;
using Application.Constants;
using Application.Repositories.EntityFramework;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetAcceptedOfferByOfferIdQuery : IRequest<OfferDto>, ISecuredRequest
{
    public int UserId { get; set; }
    public int OfferId { get; set; }
    public string[] Roles => Array.Empty<string>();

    public class GetAcceptedOfferByOfferIdQueryHandler : IRequestHandler<GetAcceptedOfferByOfferIdQuery, OfferDto>
    {
        private readonly IOfferRepository _offerRepository;
        private readonly IMapper _mapper;

        public GetAcceptedOfferByOfferIdQueryHandler(IOfferRepository offerRepository, IMapper mapper)
        {
            _offerRepository = offerRepository;
            _mapper = mapper;
        }

        public async Task<OfferDto> Handle(GetAcceptedOfferByOfferIdQuery request, CancellationToken cancellationToken)
        {
            var getData = await _offerRepository.GetAsync(p => (p.TransportRequest.UserId == request.UserId
                || p.CorporateCustomerId == request.UserId) && p.Id == request.OfferId
                && p.IsAccepted == true,
                include: p => p
                .Include(x => x.TransportRequest)
                .ThenInclude(x => x.TransportType)
                .Include(x => x.TransportRequest)
                .ThenInclude(x => x.DateGap)
                .Include(x => x.CorporateCustomer)
                .Include(x => x.Crew)
                .ThenInclude(x => x.Employees)
                .Include(x => x.Vehicles)
            );

            var result = _mapper.Map<OfferDto>(getData);
            return result;
        }
    }
}