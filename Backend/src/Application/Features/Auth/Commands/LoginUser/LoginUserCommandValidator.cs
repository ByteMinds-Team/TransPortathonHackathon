using FluentValidation;

namespace Application.Features.Auth.Commands.LoginUserCommand;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(p => p.Email).NotEmpty().NotNull().EmailAddress();
        RuleFor(p => p.Password).NotEmpty().NotNull();
    }
}