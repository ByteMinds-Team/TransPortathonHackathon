using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Drivers.Commands.CreateDriver
{
    public class CreateDriverCommandValidator : AbstractValidator<CreateDriverCommand>
    {
        public CreateDriverCommandValidator()
        {
            RuleFor(p=>p.VehicleId).NotNull();
            RuleFor(p=>p.EmployeeId).NotNull();
        }
    }
}
