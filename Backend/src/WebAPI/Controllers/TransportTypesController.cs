using Application;
using Application.Features.TransportType.Commands;
using Application.Features.TransportType.Dtos;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransportTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransportTypesController(IMediator mediator)
        => (_mediator) = (mediator);

    [HttpPost]
    public async Task<IActionResult> CreateTransportType(CreateTransportTypeCommand createTransportTypeCommand)
    {
        createTransportTypeCommand.UserId = HttpContext.User.GetUserId();
        var result = await _mediator.Send(createTransportTypeCommand);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> ListTransportTypes() {
        return Ok(await _mediator.Send(new GetListTransportTypeQuery()));
    }
}