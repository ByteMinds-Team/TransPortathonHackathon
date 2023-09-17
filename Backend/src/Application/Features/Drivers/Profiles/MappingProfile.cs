using Application.Features.Drivers.Dtos;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Drivers.Commands.CreateDriver;
using Application.Features.Drivers.Commands.DeleteDriver;
using Application.Features.Drivers.Commands.UpdateDriver;

namespace Application.Features.Drivers.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<DeletedDriverDto, Driver>().ReverseMap();
            CreateMap<UpdatedDriverDto, Driver>().ReverseMap();
            CreateMap<CreatedDriverDto, Driver>().ReverseMap();
                
            CreateMap<CreateDriverCommand, Driver>().ReverseMap();
            CreateMap<DeleteDriverCommand, Driver>().ReverseMap();
            CreateMap<UpdateDriverCommand,Driver>().ReverseMap();
            CreateMap<GetedAllDriverByCorporateCustomerIdDto, Driver>().ReverseMap()
                .ForMember(p => p.NumberPlate, opt => opt.MapFrom(x => x.Vehicle.NumberPlate))
                .ForMember(p => p.Brand, opt => opt.MapFrom(x => x.Vehicle.Brand))
                .ForMember(p => p.Color, opt => opt.MapFrom(x => x.Vehicle.Color))
                .ForMember(p => p.ModelYear, opt => opt.MapFrom(x => x.Vehicle.ModelYear))
                .ForMember(p => p.VehicleImagePath, opt => opt.MapFrom(x => x.Vehicle.ImagePath))
                .ForMember(p => p.FullName, opt => opt.MapFrom(x => x.Employee.FullName))
                .ForMember(p => p.ProfileImagePath, opt => opt.MapFrom(x => x.Employee.ProfileImagePath));

        }
    }
}
