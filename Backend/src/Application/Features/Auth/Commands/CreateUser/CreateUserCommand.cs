using Application.Features.Auth.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Auth.Commands.CreateUserCommand;

public class CreateUserCommand : IRequest<CreatedUserDto>
{
    public string FullName { get; set; }
    public IFormFile ProfilePhoto { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
