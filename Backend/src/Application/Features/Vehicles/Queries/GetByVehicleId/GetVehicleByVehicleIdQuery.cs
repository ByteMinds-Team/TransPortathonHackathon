using Application.Common.Behaviours.Authorization;
using Application.Constants;
using Application.Features.Vehicles.Rules;
using AutoMapper;
using Domain;
using MediatR;

namespace Application;

public class GetVehicleByVehicleIdQuery : IRequest<GetVehicleByVehicleIdDto> , ISecuredRequest
{
    public int VehicleId { get; set; }

    public string[] Roles => new[] { PermissionConstant.Corporate};

    public class GetVehicleByVehicleIdQueryHandler : IRequestHandler<GetVehicleByVehicleIdQuery, GetVehicleByVehicleIdDto>
    {
        private readonly IMapper _mapper;
        private readonly VehicleBusinessRules _vehicleBusinessRules;
        private readonly IVehicleRepository _vehicleRepository;

        public GetVehicleByVehicleIdQueryHandler(IMapper mapper, IVehicleRepository vehicleRepository, VehicleBusinessRules vehicleBusinessRules)
        {
            _mapper = mapper;
            _vehicleRepository = vehicleRepository;
            _vehicleBusinessRules = vehicleBusinessRules;
        }


        public async Task<GetVehicleByVehicleIdDto> Handle(GetVehicleByVehicleIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _vehicleRepository.GetAsync(p=>p.Id == request.VehicleId);
            _vehicleBusinessRules.VehicleShouldExist(result);
            return _mapper.Map<GetVehicleByVehicleIdDto>(result);
        }
    }
}
