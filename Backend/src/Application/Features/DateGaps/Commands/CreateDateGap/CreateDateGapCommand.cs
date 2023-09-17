using Application.Common.Behaviours.Authorization;
using Application.Repositories.EntityFramework;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.DateGaps;

public class CreateDateGapCommand : IRequest<DateGapDto>, ISecuredRequest
{
    public string Gap { get; set; }

    public string[] Roles => new string[] { "admin" };

    public class CreateDateGapCommandHandler : IRequestHandler<CreateDateGapCommand, DateGapDto>
    {
        private readonly IDateGapRepository _dateGapRepository;
        private readonly IMapper _mapper;

        public CreateDateGapCommandHandler(IMapper mapper, IDateGapRepository dateGapRepository)
        {
            _mapper = mapper;
            _dateGapRepository = dateGapRepository;
        }

        public async Task<DateGapDto> Handle(CreateDateGapCommand request, CancellationToken cancellationToken)
        {
            var dateGap = _mapper.Map<DateGap>(request);
            var createdDateGap = await _dateGapRepository.AddAsync(dateGap);
            return _mapper.Map<DateGapDto>(createdDateGap);
        }
    }
}
