using Application.Common.Behaviours.Authorization;
using Application.Features.Employees.Dtos;
using Application.Repositories.EntityFramework;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Queries.GetAllByCorporateId
{
    public class GetAllEmployeeByCorporateUserIdCommand : IRequest<List<GetedAllEmployeeByCorporateUserId>>, ISecuredRequest
    {
        public int CorporateUserId { get; set; }

        public string[] Roles => Array.Empty<string>();

        public class GetAllEmployeeByCorporateUserIdCommandHandler : IRequestHandler<GetAllEmployeeByCorporateUserIdCommand,List<GetedAllEmployeeByCorporateUserId>>
        {
            private readonly IEmployeeRepository _employeeRepository;
            private readonly IMapper _mapper;

            public GetAllEmployeeByCorporateUserIdCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
            {
                _employeeRepository = employeeRepository;
                _mapper = mapper;
            }

            public async Task<List<GetedAllEmployeeByCorporateUserId>> Handle(GetAllEmployeeByCorporateUserIdCommand request, CancellationToken cancellationToken)
            {
                var data  =await _employeeRepository.GetAllAsync(p=>p.CorporateCustomerId == request.CorporateUserId);
                var result = _mapper.Map<List<GetedAllEmployeeByCorporateUserId>>(data);
                return result;
            }
        }
    }
}
