using Application.Common.Behaviours.Authorization;
using Application.Constants;
using Application.Features.Employees.Dtos;
using Application.Features.Employees.Rules;
using Application.Repositories.EntityFramework;
using AutoMapper;
using MediatR;

namespace Application.Features.Employees.Queries.GetById;

public class GetEmployeeByIdQuery : IRequest<GetEmployeeByIdDto> , ISecuredRequest
{
    public int EmployeeId { get; set; }
    public int CorporateCustomerId { get; set; }
    public string[] Roles => new[] {PermissionConstant.Corporate };
    
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery , GetEmployeeByIdDto>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<GetEmployeeByIdDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _employeeRepository.GetAsync(p =>
                p.Id == request.EmployeeId && p.CorporateCustomerId == request.CorporateCustomerId);
            EmployeeBusinessRules.checkNullData(result);
            return _mapper.Map<GetEmployeeByIdDto>(result);
        }

        
    }
}