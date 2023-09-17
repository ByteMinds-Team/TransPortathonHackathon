using Application.Common.Behaviours.Authorization;
using Application.Repositories.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.TransportRequest;

public class GetAllTransportRequestQuery : IRequest<List<ListTransportRequestDto>>, ISecuredRequest
{
    public string[] Roles => Array.Empty<string>();

    public class GetAllTransportRequestQueryHandler : IRequestHandler<GetAllTransportRequestQuery, List<ListTransportRequestDto>>
    {
        private readonly ITransportRequestRepository _transportRequestRepository;

        public GetAllTransportRequestQueryHandler(ITransportRequestRepository transportRequestRepository)
        {
            _transportRequestRepository = transportRequestRepository;
        }

        public async Task<List<ListTransportRequestDto>> Handle(GetAllTransportRequestQuery request, CancellationToken cancellationToken)
        {
            var transportRequests = await _transportRequestRepository
                .GetAllAsync(include: x => x.Include(x => x.DateGap)
                    .Include(x => x.TransportType).Include(x => x.User), orderBy: x => x.OrderByDescending(x => x.CreatedDate));

            var transportRequestListDto = transportRequests.Select(x => new ListTransportRequestDto()
            {
                Id = x.Id,
                UserId = x.UserId,
                FullName = x.User.FullName,
                Email = x.User.Email,
                Description = x.Description,
                dateGap = x.DateGap,
                TransportType = x.TransportType,
                CreatedDate = x.CreatedDate
            }).ToList();

            return transportRequestListDto;
        }
    }
}