using AutoMapper;
using Domain;

namespace Application.Features.Messages;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateMessageCommand, Message>();
    }
}