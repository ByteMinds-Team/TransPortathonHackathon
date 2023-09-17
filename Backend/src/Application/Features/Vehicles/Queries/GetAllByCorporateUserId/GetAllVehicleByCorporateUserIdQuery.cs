using Application.Common.Behaviours.Authorization;
using Application.Constants;
using Application.Repositories.EntityFramework;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Vehicles.Queries.GetAllByCorporateUserId
{
    public class GetAllVehicleByCorporateUserIdQuery : IRequest<List<VehicleListDto>>, ISecuredRequest
    {
        public int CorporateUserId { get; set; }

        public string[] Roles => new[] {PermissionConstant.Corporate }; 

        public class GetAllVehicleByCorporateUserIdQueryHandler : IRequestHandler<GetAllVehicleByCorporateUserIdQuery, List<VehicleListDto>>
        {
            private readonly IMapper _mapper;
            private readonly IVehicleRepository _vehicleRepository;

            public GetAllVehicleByCorporateUserIdQueryHandler(IMapper mapper, IVehicleRepository vehicleRepository)
            {
                _mapper = mapper;
                _vehicleRepository = vehicleRepository;
            }


            public async Task<List<VehicleListDto>> Handle(GetAllVehicleByCorporateUserIdQuery request, CancellationToken cancellationToken)
            {
                var data = await _vehicleRepository.GetAllAsync(p=>p.CorporateCustomerId == request.CorporateUserId);
               var result = _mapper.Map<List<VehicleListDto>>(data);
                return result;
            }
        }
    }
}
