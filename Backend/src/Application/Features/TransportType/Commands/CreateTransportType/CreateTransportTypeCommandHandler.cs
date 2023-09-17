using Application.Common.Behaviours.Authorization;
using Application.Features.TransportType.Dtos;
using Application.Features.TransportType.Rules;
using MediatR;

namespace Application.Features.TransportType.Commands;

public class CreateTransportTypeCommand : IRequest<CreatedTransportTypeDto>, ISecuredRequest
{
    public string Type { get; set; }
    public int UserId { get; set; }

    public string[] Roles => new[] { "admin" };
    public class CreateTransportTypeCommandHandler : IRequestHandler<CreateTransportTypeCommand, CreatedTransportTypeDto>
    {
        private readonly ITransportTypeRepository _transportTypeRepository;
        private readonly TransportTypeBusinessRules _transportTypeBusinessRules;

        public CreateTransportTypeCommandHandler(ITransportTypeRepository transportTypeRepository, TransportTypeBusinessRules transportTypeBusinessRules)
            => (_transportTypeRepository, _transportTypeBusinessRules) = (transportTypeRepository, transportTypeBusinessRules);

        public async Task<CreatedTransportTypeDto> Handle(CreateTransportTypeCommand request,
            CancellationToken cancellationToken)
        {
            await _transportTypeBusinessRules.TransportTypeShouldNotDuplicate(request.Type);

            Domain.TransportType transportTypeToSave = new()
            {
                Type = request.Type
            };

            var result = await _transportTypeRepository.AddAsync(transportTypeToSave);
            return new()
            {
                Type = result.Type
            };
        }
    }
}