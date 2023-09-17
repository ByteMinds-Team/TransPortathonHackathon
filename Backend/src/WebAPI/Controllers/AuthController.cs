using Application;
using Application.Features.Auth;
using Application.Features.Auth.Commands.CreateCorporateUser;
using Application.Features.Auth.Commands.CreateTokenWithRefreshToken;
using Application.Features.Auth.Commands.CreateUserCommand;
using Application.Features.Auth.Commands.LoginUserCommand;
using Application.Features.Auth.Queries.GetListUser;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("corporateUserInformation/{corporateUserId:int}")]
    public async Task<IActionResult> GetCorporateUserInformation([FromRoute] int corporateUserId)
    {
        return Ok(await _mediator.Send(new GetCompanyInformationQuery()
        {
            CorporateUserId = corporateUserId
        }));
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromForm] CreateUserCommand command)
    {
        var data = await _mediator.Send(command);
        return Ok(data);
    }

    [HttpPost("CorporateRegister")]
    public async Task<IActionResult> CorporateRegister([FromForm] CreateCorporateUserCommand command)
    {
        var data = await _mediator.Send(command);
        return Ok(data);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginUserCommand command)
    {
        var data = await _mediator.Send(command);
        return Ok(data);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetUserData()
    {
        var data = await _mediator.Send(new GetMyUserDataQuery()
        {
            UserId = HttpContext.User.GetUserId()
        });
        return Ok(data);
    }

    [HttpPost("RefreshToken")]
    public async Task<IActionResult> RefreshToken(CreateTokenWithRefreshTokenCommand command)
    {
        var data = await _mediator.Send(command);
        return Ok(data);
    }

    [HttpGet()]
    public async Task<IActionResult> GetAll([FromQuery] GetUserListQuery query)
    {
        var data = await _mediator.Send(query);
        return Ok(data);
    }
}