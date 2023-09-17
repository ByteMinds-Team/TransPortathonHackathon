using System.Diagnostics;
using System.Security.Claims;
using Application.Common.Behaviours.Authorization;
using Application.Common.Exceptions;
using Application.Features.Vehicles.Rules;
using Application.Services;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application;

public class CreateVehicleCommand : IRequest<CreatedVehicleDto>, ISecuredRequest
{
    public string NumberPlate { get; set; }
    public string Brand { get; set; }
    public string Color { get; set; }
    public int ModelYear { get; set; }
    public IFormFile? CarImage { get; set; }
    public int UserId { get; set; }

    public string[] Roles => new string[] { "corporate" };

    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, CreatedVehicleDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        private readonly VehicleBusinessRules _vehicleBusinessRules;

        public CreateVehicleCommandHandler(IHttpContextAccessor httpContextAccessor, IVehicleRepository vehicleRepository,
            IMapper mapper, IImageService imageService, VehicleBusinessRules vehicleBusinessRules)
            => (_httpContextAccessor, _vehicleRepository, _mapper, _imageService, _vehicleRepository, _vehicleBusinessRules)
                = (httpContextAccessor, vehicleRepository, mapper, imageService, vehicleRepository, vehicleBusinessRules);


        public async Task<CreatedVehicleDto> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicleFromDB =
                await _vehicleRepository.GetAsync(v => v.NumberPlate.ToUpper() == request.NumberPlate.ToUpper());
            _vehicleBusinessRules.VehicleWithTheSameNumberPlateShouldNotExist(vehicleFromDB);

            var vehicle = _mapper.Map<Vehicle>(request);
            vehicle.CorporateCustomerId = request.UserId;

            var imageUrl = await _imageService.UploadPhoto(request.CarImage);
            vehicle.ImagePath = imageUrl;

            var createdVehicle = await _vehicleRepository.AddAsync(vehicle);
            var createdVehicleDto = _mapper.Map<CreatedVehicleDto>(createdVehicle);

            return createdVehicleDto;
        }
    }
}
