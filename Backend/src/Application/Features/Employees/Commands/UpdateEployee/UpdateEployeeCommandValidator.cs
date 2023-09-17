using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Commands.UpdateEployee
{
    public class UpdateEployeeCommandValidator : AbstractValidator<UpdateEployeeCommand>
    {
        public UpdateEployeeCommandValidator()
        {
            RuleFor(p=>p.EmployeeId).NotNull();
            RuleFor(p=>p.ProfilePhoto).NotNull();
            RuleFor(p=>p.CorporateCustomerId).NotNull();
            RuleFor(p=>p.FullName).NotNull();

        }
    }
}
