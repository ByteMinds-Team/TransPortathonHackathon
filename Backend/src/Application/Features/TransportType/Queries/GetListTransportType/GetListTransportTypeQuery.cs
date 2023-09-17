using Application.Common.Behaviours.Authorization;
using AutoMapper;
using MediatR;

namespace Application;

public class GetListTransportTypeQuery: IRequest<List<ListTransportTypeDto>>, ISecuredRequest
{
    public string[] Roles => Array.Empty<string>();

    public class GetListTransportTypeQueryHandler : IRequestHandler<GetListTransportTypeQuery, List<ListTransportTypeDto>>
    {
        private readonly ITransportTypeRepository _transportTypeRepository;
        private readonly IMapper _mapper;

        public GetListTransportTypeQueryHandler(ITransportTypeRepository transportTypeRepository, IMapper mapper)
        {
            _transportTypeRepository = transportTypeRepository;
            _mapper = mapper;
        }

        public async Task<List<ListTransportTypeDto>> Handle(GetListTransportTypeQuery request, CancellationToken cancellationToken)
        {
            var transportTypes = await _transportTypeRepository.GetAllAsync();
            var transportTypeListDto = _mapper.Map<List<ListTransportTypeDto>>(transportTypes);

            return transportTypeListDto;
        }
    }
}
