using Application.Common.Exceptions;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.CreateTokenWithRefreshToken
{
    public class CreateTokenWithRefreshTokenCommandValidator : AbstractValidator<CreateTokenWithRefreshTokenCommand>
    {
        public CreateTokenWithRefreshTokenCommandValidator()
        {
            RuleFor(x => x.Client).NotEmpty();
            RuleFor(x => x.RefreshToken).NotEmpty();
        }

    }
}
