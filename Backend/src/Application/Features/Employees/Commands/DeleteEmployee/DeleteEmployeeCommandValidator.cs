using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
    {
        public DeleteEmployeeCommandValidator()
        {
            RuleFor(p=>p.EmployeeId).NotNull();
            RuleFor(p=>p.CorporateCustomerId).NotNull();
        }
    }
}
