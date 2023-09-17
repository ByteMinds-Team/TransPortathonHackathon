using Application.Features.Offers.Dtos;
using Application.Repositories.EntityFramework;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Offers.Queries.GetAllByUserId;

public class GetAllOfferByUserIdQuery : IRequest<List<GetAllOfferByUserIdDto>>
{
    public int UserId { get; set; }
    
    public class GetAllOfferByUserIdQueryHandler : IRequestHandler<GetAllOfferByUserIdQuery,List<GetAllOfferByUserIdDto>>
    {
        private readonly IMapper _mapper;
        private readonly IOfferRepository _offerRepository;

        public GetAllOfferByUserIdQueryHandler(IOfferRepository offerRepository, IMapper mapper)
        {
            _offerRepository = offerRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllOfferByUserIdDto>> Handle(GetAllOfferByUserIdQuery request, CancellationToken cancellationToken)
        {
            var getData = await _offerRepository.GetAllAsync(p => (p.TransportRequest.UserId == request.UserId
                                                                   || p.CorporateCustomerId == request.UserId)
                                                                  && p.IsAccepted == false,
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
            return _mapper.Map<List<GetAllOfferByUserIdDto>>(getData);
        }
    }
}