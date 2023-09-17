using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TransportType.Commands.CreateTransportType;

public class CreateTransportTypeCommandValidator : AbstractValidator<CreateTransportTypeCommand>
{
    public CreateTransportTypeCommandValidator()
    {
        RuleFor(x => x.Type).NotEmpty().NotNull();
    }
}
