using Application.Features.Crews.Commands.CreateCrew;
using Application.Features.Crews.Dtos;
using AutoMapper;
using Domain;

namespace Application.Features.Crews.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Crew,CreateCrewCommand>().ReverseMap();
        CreateMap<Crew,CreatedCrewDto>().ReverseMap();
    }
}