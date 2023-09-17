using Application.Features.Drivers.Commands.CreateDriver;
using Application.Features.Drivers.Commands.DeleteDriver;
using Application.Features.Drivers.Commands.UpdateDriver;
using Application.Features.Drivers.Dtos;
using Application.Features.Drivers.Queries.GetAllByCorporateCustomerId;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DriversController : ControllerBase
{
    private readonly IMediator _mediator;

    public DriversController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreateDriver")]
    public async Task<IActionResult> AddDriver([FromForm]DriverCreateRequestDto requestDto)
    {
        CreateDriverCommand model = new()
        {
            VehicleId = requestDto.VehicleId,
            EmployeeId = requestDto.EmployeeId,
        };
       var result =  await _mediator.Send(model);
       return Ok(result);
    }
    
    [HttpPost("DeleteDriver")]
    public async Task<IActionResult> DeleteDriver(DriverDeleteRequestDto requestDto)
    {
        var result =  await _mediator.Send(new DeleteDriverCommand(){DriverId = requestDto.DriverId,CorporateCustomerId = HttpContext.User.GetUserId()});
        return Ok(result);
    }
    
    [HttpPost("UpdateDriver")]
    public async Task<IActionResult> UpdateDriver([FromForm]DriverUpdateRequestDto requestDto)
    {
        var result =  await _mediator.Send(new UpdateDriverCommand(){DriverId = requestDto.DriverId,CorporateCustomerId = HttpContext.User.GetUserId(),VehicleId = requestDto.VehicleId});
        return Ok(result);
    }
    
    [HttpGet("GetAllByCorporateCustomerId")]
    public async Task<IActionResult> GetAllByCorporateCustomerId([FromQuery]GetAllDriverByCorporateCustomerIdCommand requestDto)
    {
        var result =  await _mediator.Send(requestDto);
        return Ok(result);
    }
}
