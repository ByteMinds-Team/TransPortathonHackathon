using Application.Common.Behaviours.Authorization;
using Application.Constants;
using Application.Features.Crews.Dtos;
using Application.Features.Crews.Rules;
using Application.Repositories.EntityFramework;
using MediatR;

namespace Application.Features.Crews.Commands.DeleteCrew;

public class DeleteCrewCommand : IRequest<DeletedCrew> , ISecuredRequest
{
    public int CorporateCustomerId { get; set; }
    public int CrewId { get; set; }
    public string[] Roles => new[] {PermissionConstant.Corporate };
    
    public class DeleteCrewCommandHandler : IRequestHandler<DeleteCrewCommand , DeletedCrew>
    {
        private readonly ICrewRepository _crewRepository;

        public DeleteCrewCommandHandler(ICrewRepository crewRepository)
        {
            _crewRepository = crewRepository;
        }

        public async Task<DeletedCrew> Handle(DeleteCrewCommand request, CancellationToken cancellationToken)
        {
            var result =await _crewRepository.GetAsync(p=>p.Id == request.CrewId && p.CorporateCustomerId == request.CorporateCustomerId);
            CrewBusinessRules.checkNullSingleData(result);
            _crewRepository.DeleteAsync(result);
            return new(){IsSuccess = true};
        }
    }

    
}