using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Auth.Commands.CreateCorporateUser
{
    public class CreateCorporateUserCommandvalidator : AbstractValidator<CreateCorporateUserCommand>
    {
        public CreateCorporateUserCommandvalidator()
        {
            RuleFor(p => p.FullName).NotNull();
            RuleFor(p => p.Email).NotNull().EmailAddress();
            RuleFor(p => p.Password).NotNull();
            RuleFor(p => p.CompanyName).NotNull();
            RuleFor(p => p.ProfilePhoto).NotNull();
        }
    }
}
