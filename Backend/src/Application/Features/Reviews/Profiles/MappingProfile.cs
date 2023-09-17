using Application.Features.Reviews.Commands.CreateReview;
using Application.Features.Reviews.Dtos;
using AutoMapper;
using Domain;

namespace Application.Features.Reviews.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreatedReviewDto, Review>().ReverseMap();
        CreateMap<CreateReviewCommand , Review>().ReverseMap();
        CreateMap<GetedAllByCorporateCustomerIdDto, Review>().ReverseMap().
            ForMember(p=>p.ProfileImagePath, opt => opt.MapFrom(x=> x.User.ProfileImagePath)).
            ForMember(p=>p.FullName , opt => opt.MapFrom(x=>x.User.FullName));
    }
}