using Application;
using Application.Features.Messages;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessagesController : ControllerBase
{
    private readonly IMediator _mediator;

    public MessagesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMessage(CreateMessageCommand createMessageCommand)
    {
        createMessageCommand.SenderId = HttpContext.User.GetUserId();
        var result = await _mediator.Send(createMessageCommand);

        return Created("", result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllChats()
    {
        var result = await _mediator.Send(new GetAllChatQuery()
        {
            UserId = HttpContext.User.GetUserId()
        });

        return Ok(result);
    }

    [HttpGet("{opponentUserId:int}")]
    public async Task<IActionResult> GetAllMessagesByOpponentId([FromRoute] int opponentUserId)
    {
        var result = await _mediator.Send(new GetAllMessageQuery()
        {
            UserId = HttpContext.User.GetUserId(),
            OpponentUserId = opponentUserId,
        });

        return Ok(result);
    }
}