using Application.Features.Offers.Commands;
using Application.Features.Offers.Commands.CreateOffer;
using Application.Features.Offers.Commands.UpdateOffer;
using Application.Features.Offers.Dtos;
using AutoMapper;
using Domain;

namespace Application.Features.Offers.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateOfferCommand, Offer>().ReverseMap();
        CreateMap<CreatedOfferDto, Offer>().ReverseMap();

        CreateMap<UpdatedOfferDto, Offer>().ReverseMap();
        CreateMap<UpdateOfferCommand, Offer>().ReverseMap();

        CreateMap<OfferDto, Offer>()
        .ReverseMap()
        .ForMember(p => p.CompanyName, opt => opt.MapFrom(p => p.CorporateCustomer.CompanyName))
        .ForMember(p => p.CompanyImagePath, opt => opt.MapFrom(p => p.CorporateCustomer.ProfileImagePath))
        .ForMember(p => p.CorporateUserFullName, opt => opt.MapFrom(p => p.CorporateCustomer.FullName))
        .ForMember(p => p.CorporateCustomerProfilePhotoPath, opt => opt.MapFrom(p => p.CorporateCustomer.ProfileImagePath))
        .ForMember(p => p.CorporateCustomerId, opt => opt.MapFrom(x => x.CorporateCustomer.Id))
        .ForMember(p => p.TransportRequestId, opt => opt.MapFrom(x => x.TransportRequest.Id))
        .ForMember(p => p.Employees, opt => opt.MapFrom(x => x.Crew.Employees.Select(x => new Employee
        {
            Id = x.Id,
            CorporateCustomerId = x.CorporateCustomerId,
            FullName = x.FullName,
            ProfileImagePath = x.ProfileImagePath
        })))
        .ForMember(p => p.CrewId, opt => opt.MapFrom(x => x.Crew.Id))
        .ForMember(p => p.Vehicles, opt => opt.MapFrom(x => x.Vehicles.Select(x => new Vehicle
        {
            Id = x.Id,
            CorporateCustomerId = x.CorporateCustomerId,
            Brand = x.Brand,
            Color = x.Color,
            CreatedDate = x.CreatedDate,
            ImagePath = x.ImagePath,
            ModelYear = x.ModelYear,
            NumberPlate = x.NumberPlate,
            UpdatedDate = x.UpdatedDate
        })));

        CreateMap<GetedAllAcceptedOfferByUserIdDto, Offer>()
        .ReverseMap()
        .ForMember(p => p.CompanyName, opt => opt.MapFrom(p => p.CorporateCustomer.CompanyName))
        .ForMember(p => p.CompanyImagePath, opt => opt.MapFrom(p => p.CorporateCustomer.ProfileImagePath))
        .ForMember(p => p.CorporateUserFullName, opt => opt.MapFrom(p => p.CorporateCustomer.FullName))
        .ForMember(p => p.CorporateCustomerId, opt => opt.MapFrom(x => x.CorporateCustomer.Id))
        .ForMember(p => p.CorporateCustomerProfilePhotoPath, opt => opt.MapFrom(p => p.CorporateCustomer.ProfileImagePath))
        .ForMember(p => p.TransportRequestId, opt => opt.MapFrom(x => x.TransportRequest.Id))
        .ForMember(p => p.Employees, opt => opt.MapFrom(x => x.Crew.Employees.Select(x => new Employee
        {
            Id = x.Id,
            CorporateCustomerId = x.CorporateCustomerId,
            FullName = x.FullName,
            ProfileImagePath = x.ProfileImagePath
        })))
        .ForMember(p => p.CrewId, opt => opt.MapFrom(x => x.Crew.Id))
        .ForMember(p => p.Vehicles, opt => opt.MapFrom(x => x.Vehicles.Select(x => new Vehicle
        {
            Id = x.Id,
            CorporateCustomerId = x.CorporateCustomerId,
            Brand = x.Brand,
            Color = x.Color,
            CreatedDate = x.CreatedDate,
            ImagePath = x.ImagePath,
            ModelYear = x.ModelYear,
            NumberPlate = x.NumberPlate,
            UpdatedDate = x.UpdatedDate
        })));
        
        
        
        
        
        
        CreateMap<GetAllOfferByUserIdDto, Offer>()
            .ReverseMap()
            .ForMember(p => p.CompanyName, opt => opt.MapFrom(p => p.CorporateCustomer.CompanyName))
            .ForMember(p => p.CompanyImagePath, opt => opt.MapFrom(p => p.CorporateCustomer.ProfileImagePath))
            .ForMember(p => p.CorporateUserFullName, opt => opt.MapFrom(p => p.CorporateCustomer.FullName))
            .ForMember(p => p.CorporateCustomerId, opt => opt.MapFrom(x => x.CorporateCustomer.Id))
            .ForMember(p => p.CorporateCustomerProfilePhotoPath, opt => opt.MapFrom(p => p.CorporateCustomer.ProfileImagePath))
            .ForMember(p => p.TransportRequestId, opt => opt.MapFrom(x => x.TransportRequest.Id))
            .ForMember(p => p.Employees, opt => opt.MapFrom(x => x.Crew.Employees.Select(x => new Employee
            {
                Id = x.Id,
                CorporateCustomerId = x.CorporateCustomerId,
                FullName = x.FullName,
                ProfileImagePath = x.ProfileImagePath
            })))
            .ForMember(p => p.CrewId, opt => opt.MapFrom(x => x.Crew.Id))
            .ForMember(p => p.Vehicles, opt => opt.MapFrom(x => x.Vehicles.Select(x => new Vehicle
            {
                Id = x.Id,
                CorporateCustomerId = x.CorporateCustomerId,
                Brand = x.Brand,
                Color = x.Color,
                CreatedDate = x.CreatedDate,
                ImagePath = x.ImagePath,
                ModelYear = x.ModelYear,
                NumberPlate = x.NumberPlate,
                UpdatedDate = x.UpdatedDate
            })));
    }
}