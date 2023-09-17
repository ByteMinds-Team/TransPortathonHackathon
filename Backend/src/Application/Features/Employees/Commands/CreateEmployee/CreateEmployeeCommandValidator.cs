using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(p=>p.ProfilePhoto).NotNull();
            RuleFor(p=>p.FullName).NotNull();
            RuleFor(p=>p.CorporateCustomerId).NotNull();
        }
    }
}
