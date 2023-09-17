using AutoMapper;
using Domain;

namespace Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {  
        CreateMap<TransportType, ListTransportTypeDto>().ReverseMap();
    }
}
