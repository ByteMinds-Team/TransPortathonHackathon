using Application.Features.Drivers.Dtos;
using Application.Features.Drivers.Rules;
using Application.Repositories.EntityFramework;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Drivers.Queries.GetAllByCorporateCustomerId
{
    public class GetAllDriverByCorporateCustomerIdCommand : IRequest<List<GetedAllDriverByCorporateCustomerIdDto>>
    {
        public int CorporateCustomerId { get; set; }

        public class GetAllDriverByCorporateCustomerIdCommandHandler : IRequestHandler<GetAllDriverByCorporateCustomerIdCommand, List<GetedAllDriverByCorporateCustomerIdDto>>
        {
            private readonly IMapper _mapper;
            private readonly IDriverRepository _repository;

            public GetAllDriverByCorporateCustomerIdCommandHandler(IMapper mapper, IDriverRepository repository)
            {
                _mapper = mapper;
                _repository = repository;
            }

            public async Task<List<GetedAllDriverByCorporateCustomerIdDto>> Handle(GetAllDriverByCorporateCustomerIdCommand request, CancellationToken cancellationToken)
            {
                var gettedData = await _repository.GetAllAsync(p=>p.Employee.CorporateCustomerId == request.CorporateCustomerId , include : p=> p.Include(x=>x.Vehicle).Include(x=>x.Employee));
                DriverBusinessRules.MultiCheckIfNull(gettedData);
                return _mapper.Map<List<GetedAllDriverByCorporateCustomerIdDto>>(gettedData);
            }
        }
    }

}
