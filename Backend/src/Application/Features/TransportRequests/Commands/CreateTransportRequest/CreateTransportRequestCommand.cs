using Application.Common.Behaviours.Authorization;
using Application.Repositories.EntityFramework;
using AutoMapper;
using MediatR;

namespace Application.Features.TransportRequest;

public class CreateTransportRequestCommand : IRequest<CreatedTransportRequestDto>, ISecuredRequest
{
    public string Description { get; set; }
    public int UserId { get; set; }
    public int DateGapId { get; set; }
    public int TransportTypeId { get; set; }

    public string[] Roles => Array.Empty<string>();

    public class CreateTransportRequestCommandHandler : IRequestHandler<CreateTransportRequestCommand, CreatedTransportRequestDto>
    {
        private readonly IMapper _mapper;
        private readonly ITransportRequestRepository _transportRequestRepository;

        public CreateTransportRequestCommandHandler(ITransportRequestRepository transportRequestRepository, IMapper mapper)
        {
            _transportRequestRepository = transportRequestRepository;
            _mapper = mapper;
        }

        public async Task<CreatedTransportRequestDto> Handle(CreateTransportRequestCommand request, CancellationToken cancellationToken)
        {
            var transportRequest = _mapper.Map<Domain.TransportRequest>(request);

            var createdTransportRequest = await _transportRequestRepository.AddAsync(transportRequest);

            return _mapper.Map<CreatedTransportRequestDto>(createdTransportRequest);
        }
    }
}