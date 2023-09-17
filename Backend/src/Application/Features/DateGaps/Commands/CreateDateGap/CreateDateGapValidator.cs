using Application.Features.DateGaps;
using FluentValidation;

namespace Application.Features.DateGaps;

public class CreateDateGapValidator: AbstractValidator<CreateDateGapCommand> {
    public CreateDateGapValidator()
    {
        RuleFor(d => d.Gap).NotEmpty().NotNull();
    }
}