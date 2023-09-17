using Application.Common.Behaviours.Authentication;
using Application.Common.Behaviours.Authorization;
using Application.Features.Drivers.Dtos;
using Application.Repositories.EntityFramework;
using Application.Services;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Constants;
namespace Application.Features.Drivers.Commands.CreateDriver
{
    public class CreateDriverCommand : IRequest<CreatedDriverDto>, ISecuredRequest
    {
        public int VehicleId { get; set; }
        public int EmployeeId { get; set; }

        public string[] Roles => new[] {PermissionConstant.Corporate };

        public class CreateDriverCommandHandler : IRequestHandler<CreateDriverCommand, CreatedDriverDto>
        {
            private readonly IDriverRepository _driverRepository;
            private readonly IMapper _mapper;
            public CreateDriverCommandHandler(IDriverRepository driverRepository, IMapper mapper)
            {
                _driverRepository = driverRepository;
                _mapper = mapper;
            }


            public async Task<CreatedDriverDto> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
            {
                var mappedData = _mapper.Map<Driver>(request);
                var data =await _driverRepository.AddAsync(mappedData);
                return new() { IsSuccess = true };
            }
        }
    }
}
