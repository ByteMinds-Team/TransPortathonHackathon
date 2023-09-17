using Application.Common.Paging;
using Application.Features.Auth.Dtos;
using Application.Features.Vehicles.Models;
using AutoMapper;
using Domain;

namespace Application.Features.Vehicles.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<VehicleListModel, IPaginate<Vehicle>>().ReverseMap();
        CreateMap<Vehicle, VehicleListDto>().ReverseMap().ForMember(p=>p.Id , opt => opt.MapFrom(x=>x.Id));

        CreateMap<CreatedVehicleDto, Vehicle>().ReverseMap();
        CreateMap<CreateVehicleCommand, Vehicle>().ReverseMap();

        CreateMap<GetVehicleByVehicleIdDto, Vehicle>().ReverseMap();
    }
}