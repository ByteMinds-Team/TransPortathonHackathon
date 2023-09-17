using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Vehicles.Queries.GetAllByCorporateUserId
{
    public class GetAllVehicleByCorporateUserIdQueryValidator : AbstractValidator<GetAllVehicleByCorporateUserIdQuery>
    {
        public GetAllVehicleByCorporateUserIdQueryValidator()
        {
            RuleFor(p=>p.CorporateUserId).NotNull();
        }
    }
}
