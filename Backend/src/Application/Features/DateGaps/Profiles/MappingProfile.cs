using AutoMapper;
using Domain;

namespace Application.Features.DateGaps;

public class MappingProfile : Profile {
    public MappingProfile()
    {
        CreateMap<DateGap, DateGapDto>().ReverseMap();
        CreateMap<DateGap, CreateDateGapCommand>().ReverseMap();
    }
}