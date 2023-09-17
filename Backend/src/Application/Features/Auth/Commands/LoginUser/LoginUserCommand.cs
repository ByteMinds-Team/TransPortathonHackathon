using Application.Features.Auth.Dtos;
using MediatR;

namespace Application.Features.Auth.Commands.LoginUserCommand;

public class LoginUserCommand : IRequest<LoginedUserDto>
{
    public string Email { get; set; }
    public string Password { get; set; }
}