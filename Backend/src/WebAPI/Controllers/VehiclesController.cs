using Application;
using Application.Features.Vehicles.Queries.GetAllByCorporateUserId;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI;

[Route("api/[controller]")]
[ApiController]
public class VehiclesController : ControllerBase
{
    private readonly IMediator _mediator;

    public VehiclesController(IMediator mediator)
        => (_mediator) = (mediator);

    [HttpPost()]
    public async Task<IActionResult> CreateVehicle([FromForm]VehicleRequestDto vehicleRequestDto)
    {
        CreateVehicleCommand createVehicleCommand = new()
        {
            NumberPlate = vehicleRequestDto.NumberPlate,
            UserId = HttpContext.User.GetUserId(),
            Brand = vehicleRequestDto.Brand,
            CarImage = vehicleRequestDto.CarImage,
            Color = vehicleRequestDto.Color,
            ModelYear = vehicleRequestDto.ModelYear
        };
        var result = await _mediator.Send(createVehicleCommand);
        return Ok(result);
    }

    [HttpGet()]
    public async Task<IActionResult> GetAllVehicleByCorporateUserId() {
        var result = await _mediator.Send(new GetAllVehicleByCorporateUserIdQuery() { CorporateUserId = HttpContext.User.GetUserId()});
        return Ok(result);
    }

    [HttpGet("GetVehicleById")]
    public async Task<IActionResult> GetAllVehicleByCorporateUserId([FromQuery] int vehicleId) {
        var result = await _mediator.Send(new GetVehicleByVehicleIdQuery() { VehicleId = vehicleId});
        return Ok(result);
    }
}
