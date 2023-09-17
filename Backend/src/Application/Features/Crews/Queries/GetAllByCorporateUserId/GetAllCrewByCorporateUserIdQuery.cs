using Application.Common.Behaviours.Authorization;
using Application.Common.Exceptions;
using Application.Constants;
using Application.Features.Crews.Dtos;
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

namespace Application.Features.Crews.Queries.GetAllByCorporateUserId
{
    public class GetAllCrewByCorporateUserIdQuery : IRequest<List<GetedAllCrewByCorporateUserIdDto>>, ISecuredRequest
    {
        public int CorporateCustomerId { get; set; }
        public string[] Roles => new[] {PermissionConstant.Corporate };

        public class GetAllCrewByCorporateUserIdQueryHandler : IRequestHandler<GetAllCrewByCorporateUserIdQuery, List<GetedAllCrewByCorporateUserIdDto>>
        {
            private readonly IMapper _mapper;
            private readonly ICrewRepository _crewRepository;
            public GetAllCrewByCorporateUserIdQueryHandler(IMapper mapper, ICrewRepository crewRepository)
            {
                _mapper = mapper;
                _crewRepository = crewRepository;
            }


            public async Task<List<GetedAllCrewByCorporateUserIdDto>> Handle(GetAllCrewByCorporateUserIdQuery request, CancellationToken cancellationToken)
            {
                var result = await _crewRepository.GetAllAsync(p=> p.CorporateCustomerId == request.CorporateCustomerId,include : p => p.Include(x=>x.Employees));
                return result.Select(p => new GetedAllCrewByCorporateUserIdDto() {Employees = p.Employees.Select(x=> new Employee() {
                    FullName = x.FullName ,
                    ProfileImagePath = x.ProfileImagePath,
                    Id = x.Id
                
                }).ToList(),CrewName = p.Name , Id = p.Id}).ToList();
            }
        }
    }
}
