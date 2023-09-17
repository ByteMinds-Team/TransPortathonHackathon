using Application.Common.Behaviours.Authorization;
using Application.Common.Exceptions;
using Application.Repositories.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application;

public class GetCompanyInformationQuery : IRequest<CorporateCustomerInformation>, ISecuredRequest
{
    public int CorporateUserId { get; set; }
    public string[] Roles => Array.Empty<string>();

    public class GetCompanyInformationQueryhandler : IRequestHandler<GetCompanyInformationQuery, CorporateCustomerInformation>
    {
        private readonly ICorporateUserRepository _corporateUserRepository;
        private readonly IReviewRepository _reviewRepository;

        public GetCompanyInformationQueryhandler(ICorporateUserRepository corporateUserRepository, IReviewRepository reviewRepository)
        {
            _corporateUserRepository = corporateUserRepository;
            _reviewRepository = reviewRepository;
        }

        public async Task<CorporateCustomerInformation> Handle(GetCompanyInformationQuery request, CancellationToken cancellationToken)
        {
            var result = await _corporateUserRepository.GetAsync(
                x => x.Id == request.CorporateUserId,
                include: x => x.Include(x => x.Vehicles)
                .Include(x => x.Crews)
                .Include(x => x.Employees)
            ) ?? throw new BusinessException("Corporate Customer with given id does not exist");

            var reviews = await _reviewRepository.GetAllAsync(x => x.CorporateCustomerId == request.CorporateUserId);

            return new()
            {
                Id = result.Id,
                CompanyName = result.CompanyName,
                Email = result.Email,
                FullName = result.FullName,
                ProfileImagePath = result.ProfileImagePath,
                Vehicles = result.Vehicles.Select(x =>
                {
                    x.Offers = null;
                    x.CorporateCustomer = null;
                    return x;
                }).ToList(),
                Crews = result.Crews.Select(x =>
                {
                    x.CorporateCustomer = null;
                    x.Employees = null;
                    return x;
                }).ToList(),
                Employees = result.Employees.Select(x =>
                {
                    x.CorporateCustomer = null;
                    x.Crews = null;
                    x.Drivers = null;
                    return x;
                }).ToList(),
            };
        }
    }
}
