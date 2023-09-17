using Application.Features.Reviews.Dtos;
using Application.Repositories.EntityFramework;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Reviews.Queries.GetAllByCorporateCustomerId;

public class GetAllReviewByCorporateCustomerIdCommand : IRequest<List<GetedAllByCorporateCustomerIdDto>>
{
    public int CorporateCustomerId { get; set; }
    
    public class GetAllByCorporateCustomerIdCommandHandler : IRequestHandler<GetAllReviewByCorporateCustomerIdCommand,List<GetedAllByCorporateCustomerIdDto>>
    {
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;

        public GetAllByCorporateCustomerIdCommandHandler(IMapper mapper, IReviewRepository reviewRepository)
        {
            _mapper = mapper;
            _reviewRepository = reviewRepository;
        }

        public async Task<List<GetedAllByCorporateCustomerIdDto>> Handle(GetAllReviewByCorporateCustomerIdCommand request, CancellationToken cancellationToken)
        {
            var data = await _reviewRepository.GetAllAsync(p => p.CorporateCustomerId == request.CorporateCustomerId,include: p=>p.Include(x=>x.User));
            return _mapper.Map<List<GetedAllByCorporateCustomerIdDto>>(data);
        }
    }
    
}