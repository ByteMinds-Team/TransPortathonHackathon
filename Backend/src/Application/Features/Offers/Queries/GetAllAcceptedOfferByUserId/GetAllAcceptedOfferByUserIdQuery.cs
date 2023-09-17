using Application.Common.Behaviours.Authorization;
using Application.Features.Offers.Dtos;
using Application.Repositories.EntityFramework;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Offers.Queries.GetAllAcceptedOfferByUserId;

public class GetAllAcceptedOfferByUserIdQuery : IRequest<List<GetedAllAcceptedOfferByUserIdDto>>, ISecuredRequest
{
    public int UserId { get; set; }

    public string[] Roles => Array.Empty<string>();

    public class GetAllAcceptedOfferByUserIdQueryHandler : IRequestHandler<GetAllAcceptedOfferByUserIdQuery, List<GetedAllAcceptedOfferByUserIdDto>>
    {
        private readonly IMapper _mapper;
        private readonly IOfferRepository _offerRepository;

        public GetAllAcceptedOfferByUserIdQueryHandler(IMapper mapper, IOfferRepository offerRepository)
        {
            _mapper = mapper;
            _offerRepository = offerRepository;
        }

        public async Task<List<GetedAllAcceptedOfferByUserIdDto>> Handle(GetAllAcceptedOfferByUserIdQuery request, CancellationToken cancellationToken)
        {
            var getData = await _offerRepository.GetAllAsync(p => (p.TransportRequest.UserId == request.UserId
                || p.CorporateCustomerId == request.UserId)
                && p.IsAccepted == true,
                include: p => p
                .Include(x => x.TransportRequest)
                .ThenInclude(x => x.TransportType)
                .Include(x => x.TransportRequest)
                .ThenInclude(x => x.DateGap)
                .Include(x => x.CorporateCustomer)
                .Include(x => x.Crew)
                .ThenInclude(x => x.Employees)
                .Include(x => x.Vehicles),
                enableTracking: false);
            return _mapper.Map<List<GetedAllAcceptedOfferByUserIdDto>>(getData);
        }
    }
}