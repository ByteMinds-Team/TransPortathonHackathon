using Application.Common.Behaviours.Authorization;
using Application.Constants;
using Application.Features.Crews.Dtos;
using Application.Features.Crews.Rules;
using Application.Repositories.EntityFramework;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.Crews.Commands.CreateCrew;

public class CreateCrewCommand : IRequest<CreatedCrewDto> , ISecuredRequest
{
    public string Name { get; set; }
    public List<int> EmployeeIds { get; set; }
    public int CorporateCustomerId { get; set; }
    public string[] Roles => new[] { PermissionConstant.Corporate };
    
    public class CreateCrewCommandHandler : IRequestHandler<CreateCrewCommand,CreatedCrewDto>
    {
        private readonly ICrewRepository _crewRepository;
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;

        public CreateCrewCommandHandler(ICrewRepository crewRepository, IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _crewRepository = crewRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<CreatedCrewDto> Handle(CreateCrewCommand request, CancellationToken cancellationToken)
        {
           var employees = await _employeeRepository.GetAllAsync(p=>request.EmployeeIds.Any(id=>id == p.Id));
           CrewBusinessRules.checkEmployeeNullData(employees);
           var mappedReq = _mapper.Map<Crew>(request);
           mappedReq.Employees = employees;
           mappedReq.Employees = employees;
           var returnData = await _crewRepository.AddAsync(mappedReq);
           return _mapper.Map<CreatedCrewDto>(returnData);
        }
    }

    
}