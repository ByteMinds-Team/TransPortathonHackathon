using Application.Features.Reviews.Dtos;
using Application.Repositories.EntityFramework;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Behaviours.Authorization;
using Application.Features.Reviews.Rules;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewCommand : IRequest<CreatedReviewDto> , ISecuredRequest{
        public int Point { get; set; }
        public string Comment { get; set; }
        public int OfferId { get; set; }
        public int UserId { get; set; }

        public string[] Roles => Array.Empty<string>();
        //TODO write Req dto
        public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, CreatedReviewDto>
        {
            private readonly IMapper _mapper;
            private readonly IReviewRepository _reviewRepository;
            private readonly IOfferRepository _offerRepository;

            public CreateReviewCommandHandler(IMapper mapper, IReviewRepository reviewRepository, IOfferRepository offerRepository)
            {
                _mapper = mapper;
                _reviewRepository = reviewRepository;
                _offerRepository = offerRepository;
            }

            public async Task<CreatedReviewDto> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
            {
                var offer = await _offerRepository.GetAsync(p=>p.IsAccepted == true && p.Id == request.OfferId 
                    && p.TransportRequest.UserId == request.UserId , include: p=>p.Include(x=>x.TransportRequest)
                    );
                ReviewsBusinessRules.ThrowErrorIfOfferIsNull(offer);
                ReviewsBusinessRules.ThrowErrorIfOfferDate(offer);
                var mappedData = _mapper.Map<Review>(request);
                mappedData.CorporateCustomerId = offer.CorporateCustomerId;
                var result = await _reviewRepository.AddAsync(mappedData);
                return _mapper.Map<CreatedReviewDto>(result);
            }
        }

        
    }
}
