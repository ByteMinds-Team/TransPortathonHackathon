using Application.Features.TransportRequest;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransportRequestsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransportRequestsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransportRequest(CreateTransportRequestCommand createTransportRequestCommand) {
        createTransportRequestCommand.UserId = HttpContext.User.GetUserId();
        var result = await _mediator.Send(createTransportRequestCommand);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTransportRequest() {
        var result = await _mediator.Send(new GetAllTransportRequestQuery());
        return Ok(result);
    }
}