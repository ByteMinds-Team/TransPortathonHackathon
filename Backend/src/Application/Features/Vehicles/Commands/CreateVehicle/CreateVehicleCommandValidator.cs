using FluentValidation;

namespace Application;

public class CreateVehicleCommandValidator: AbstractValidator<CreateVehicleCommand>
{
    public CreateVehicleCommandValidator()
    {
        RuleFor(v => v.Brand).NotEmpty().NotNull();
        RuleFor(v => v.Color).NotEmpty().NotNull();
        RuleFor(v => v.ModelYear).NotEmpty().NotNull().GreaterThan(0);
        RuleFor(v => v.NumberPlate).NotEmpty().NotNull();
    }
}