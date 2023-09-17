using Application.Features.Employees.Queries.GetAllByCorporateId;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Queries.GetAllByCorporateUserId
{
    public class GetAllEmployeeByCorporateUserIdCommandValidator : AbstractValidator<GetAllEmployeeByCorporateUserIdCommand>
    {
        public GetAllEmployeeByCorporateUserIdCommandValidator()
        {
            RuleFor(p=>p.CorporateUserId).NotNull();
        }
    }
}
