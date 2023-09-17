using Application.Common.Behaviours.Authentication;
using Application.Common.Behaviours.Authorization;
using Application.Constants;
using Application.Features.Drivers.Dtos;
using Application.Features.Drivers.Rules;
using Application.Repositories.EntityFramework;
using Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Drivers.Commands.UpdateDriver;

public class UpdateDriverCommand : IRequest<UpdatedDriverDto>, ISecuredRequest
{
    public int DriverId { get; set; }
    public int VehicleId { get; set; }
    public int CorporateCustomerId { get; set; }

    public string[] Roles => new[] { PermissionConstant.Corporate };

    public class UpdateDriverCommandHandler : IRequestHandler<UpdateDriverCommand, UpdatedDriverDto>
    {
        private readonly IMapper _mapper;
        private readonly IDriverRepository _driverRepository;
        private readonly IImageService _imageService;

        public UpdateDriverCommandHandler(IMapper mapper, IDriverRepository driverRepository, IImageService imageService)
        {
            _mapper = mapper;
            _driverRepository = driverRepository;
            _imageService = imageService;
        }

        public async Task<UpdatedDriverDto> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
        {
            var getedData =await _driverRepository.GetAsync(p=> p.EmployeeId == request.DriverId && p.Employee.CorporateCustomerId == request.CorporateCustomerId, include : x=>x.Include(p=>p.Employee));
            DriverBusinessRules.CheckIfNull(getedData);
            var updatedData =await _driverRepository.UpdateAsync(_mapper.Map(request,getedData));
            return _mapper.Map<UpdatedDriverDto>(updatedData);
        }
    }
}
