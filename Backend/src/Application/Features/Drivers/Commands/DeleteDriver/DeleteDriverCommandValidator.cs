using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Drivers.Commands.DeleteDriver
{
    public class DeleteDriverCommandValidator : AbstractValidator<DeleteDriverCommand>
    {
        public DeleteDriverCommandValidator()
        {
            RuleFor(p=>p.DriverId).NotNull();
            RuleFor(p=>p.CorporateCustomerId).NotNull();
        }
    }
}
