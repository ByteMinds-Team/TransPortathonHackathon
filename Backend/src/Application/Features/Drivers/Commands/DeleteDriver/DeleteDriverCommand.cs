using Application.Common.Behaviours.Authorization;
using Application.Constants;
using Application.Features.Drivers.Dtos;
using Application.Features.Drivers.Rules;
using Application.Repositories.EntityFramework;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Drivers.Commands.DeleteDriver
{
    public class DeleteDriverCommand : IRequest<DeletedDriverDto>, ISecuredRequest
    {
        public int CorporateCustomerId { get; set; }
        public int DriverId { get; set; }

        public string[] Roles => new[] { PermissionConstant.Corporate };

        public class DeleteDriverCommandHandler : IRequestHandler<DeleteDriverCommand, DeletedDriverDto>
        {
            private readonly IMapper _mapper;
            private readonly IDriverRepository _driverRepository;

            public DeleteDriverCommandHandler(IMapper mapper, IDriverRepository driverRepository)
            {
                _mapper = mapper;
                _driverRepository = driverRepository;
            }

            public async Task<DeletedDriverDto> Handle(DeleteDriverCommand request, CancellationToken cancellationToken)
            {
                var gettedData = await _driverRepository.GetAsync(P=> P.EmployeeId == request.DriverId && P.Employee.CorporateCustomerId == request.CorporateCustomerId , include : x => x.Include(u=>u.Employee));
                DriverBusinessRules.CheckIfNull(gettedData);
                var deletedData = await _driverRepository.DeleteAsync(gettedData);
                return _mapper.Map<DeletedDriverDto>(deletedData);
            }
        }
    }
}
