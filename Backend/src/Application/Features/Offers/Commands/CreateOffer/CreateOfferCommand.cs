using Application.Common.Behaviours.Authorization;
using Application.Common.Exceptions;
using Application.Constants;
using Application.Features.Offers.Dtos;
using Application.Repositories.EntityFramework;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.Offers.Commands.CreateOffer;

public class CreateOfferCommand : IRequest<CreatedOfferDto>, ISecuredRequest
{
    public double Price { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Description { get; set; }
    public int CorporateCustomerId { get; set; }
    public int TransportRequestId { get; set; }
    public int CrewId { get; set; }

    public int VehicleId { get; set; }


    public string[] Roles => new[] { PermissionConstant.Corporate };

    public class CreateOfferCommandHandler : IRequestHandler<CreateOfferCommand, CreatedOfferDto>
    {
        private readonly IMapper _mapper;
        private readonly IOfferRepository _offerRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public CreateOfferCommandHandler(IMapper mapper, IOfferRepository offerRepository, IVehicleRepository vehicleRepository)
        {
            _mapper = mapper;
            _offerRepository = offerRepository;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<CreatedOfferDto> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetAsync(p => p.Id == request.VehicleId);
            if (vehicle is null) throw new BusinessException("Vehicle Not Found");

            var mappedReq = _mapper.Map<Offer>(request);
            mappedReq.Vehicles = new[] { vehicle };
            var returnData = await _offerRepository.AddAsync(mappedReq);
            return _mapper.Map<CreatedOfferDto>(returnData);
        }
    }
}