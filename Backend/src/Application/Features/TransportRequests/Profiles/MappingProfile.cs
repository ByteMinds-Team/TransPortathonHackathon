using AutoMapper;

namespace Application.Features.TransportRequest;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.TransportRequest, CreateTransportRequestCommand>().ReverseMap();
        CreateMap<Domain.TransportRequest, CreatedTransportRequestDto>().ReverseMap();
    }
}