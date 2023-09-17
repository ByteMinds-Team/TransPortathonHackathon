using Application.Repositories.EntityFramework;
using FluentValidation;

namespace Application.Features.Auth.Commands.CreateUserCommand;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(p => p.Email).NotEmpty().NotNull().EmailAddress();
        RuleFor(p => p.Password).NotEmpty().NotNull();
        RuleFor(p => p.FullName).NotEmpty().NotNull();
        RuleFor(p => p.ProfilePhoto).NotEmpty().NotNull();
    }
}