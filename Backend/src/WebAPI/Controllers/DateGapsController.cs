using Application.Features.DateGaps;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DateGapsController : ControllerBase {
    private readonly IMediator _mediator;

    public DateGapsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDateGap(CreateDateGapCommand createDateGapCommand) {
        var result = await _mediator.Send(createDateGapCommand);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDateGap() {
        var result = await _mediator.Send(new GetAllDateGapQuery());
        return Ok(result);
    }
}