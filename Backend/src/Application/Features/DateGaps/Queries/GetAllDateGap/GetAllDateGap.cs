using Application.Common.Behaviours.Authorization;
using Application.Repositories.EntityFramework;
using AutoMapper;
using MediatR;

namespace Application.Features.DateGaps;

public class GetAllDateGapQuery: IRequest<List<DateGapDto>>, ISecuredRequest {
    public string[] Roles => Array.Empty<string>();

    public class GetAllDateGapQueryHandler : IRequestHandler<GetAllDateGapQuery, List<DateGapDto>>
    {
        private readonly IDateGapRepository _dateGapRepository;
        private readonly IMapper _mapper;

        public GetAllDateGapQueryHandler(IDateGapRepository dateGapRepository, IMapper mapper)
        {
            _dateGapRepository = dateGapRepository;
            _mapper = mapper;
        }

        public async Task<List<DateGapDto>> Handle(GetAllDateGapQuery request, CancellationToken cancellationToken)
        {
            var dateGaps = await _dateGapRepository.GetAllAsync();
            return _mapper.Map<List<DateGapDto>>(dateGaps);
        }
    }
}