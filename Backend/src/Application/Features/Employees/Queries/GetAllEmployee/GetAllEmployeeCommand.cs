using Application.Common.Behaviours.Authorization;
using Application.Constants;
using Application.Features.Employees.Dtos;
using Application.Repositories.EntityFramework;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Employees.Queries.GetAllEmployee
{
    public class GetAllEmployeeCommand : IRequest<IList<GetedAllEmployee>>, ISecuredRequest
    {
        public int CorporateUserId { get; set; }

        public string[] Roles => new[] {PermissionConstant.Corporate };

        public class GetAllEmployeeCommandHandler : IRequestHandler<GetAllEmployeeCommand, IList<GetedAllEmployee>>
        {
            private readonly IEmployeeRepository _employeeRepository;
            private readonly IMapper _mapper;
            public GetAllEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
            {
                _employeeRepository = employeeRepository;
                _mapper = mapper;
            }

            public async Task<IList<GetedAllEmployee>> Handle(GetAllEmployeeCommand request, CancellationToken cancellationToken)
            {
                var data =  await _employeeRepository.GetAllAsync(p=>p.CorporateCustomerId == request.CorporateUserId, include: p => p.Include(x => x.Drivers).ThenInclude(x=>x.Vehicle));
                var result = data.Select(p => new GetedAllEmployee() { Id = p.Id,FullName = p.FullName, ProfileImagePath = p.ProfileImagePath, vehicle = p.Drivers.Select(x=>x.Vehicle).ToList() }).ToList();
                //var result = _mapper.Map<List<GetedAllEmployee>>(data);
                return result;
            }
        }
    }
}
