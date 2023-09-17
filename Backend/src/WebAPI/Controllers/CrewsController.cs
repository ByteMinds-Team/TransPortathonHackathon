using Application.Features.Crews.Commands.CreateCrew;
using Application.Features.Crews.Commands.DeleteCrew;
using Application.Features.Crews.Dtos;
using Application.Features.Crews.Queries.GetAllByCorporateUserId;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CrewsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CrewsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCrew(CrewCreateRequestDto requestDto)
    {
        var result =await _mediator.Send(new CreateCrewCommand(){Name = requestDto.Name,EmployeeIds = requestDto.EmployeeIds,CorporateCustomerId = HttpContext.User.GetUserId()});
        return Ok(result);
    }
    
    [HttpPost("DeleteCrew")]
    public async Task<IActionResult> DeleteCrew([FromQuery]int crewId)
    {
        var result =await _mediator.Send(new DeleteCrewCommand(){CrewId = crewId,CorporateCustomerId = HttpContext.User.GetUserId()});
        return Ok(result);
    }

    [HttpGet("GetAllCrewByCorporateUserId")]
    public async Task<IActionResult> GetAllCrewByCorporateUserId()
    {
        var result = await _mediator.Send(new GetAllCrewByCorporateUserIdQuery() {CorporateCustomerId = HttpContext.User.GetUserId()});
        return Ok(result);
    }
}