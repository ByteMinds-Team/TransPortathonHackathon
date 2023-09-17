using Application.Features.Offers.Commands.AcceptOffer;
using Application.Features.Offers.Commands.CancelOffer;
using Application.Features.Offers.Commands.CreateOffer;
using Application.Features.Offers.Dtos;
using Application.Features.Offers.Queries.GetAllAcceptedOfferByUserId;
using Application.Features.Offers.Queries.GetAllByUserId;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OffersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OffersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreateOffer")]
    public async Task<IActionResult> CreateOffer(OfferCreateRequestDto requestDto)
    {
        var result = await _mediator.Send(new CreateOfferCommand() { VehicleId = requestDto.VehicleId, Description = requestDto.Description, AppointmentDate = requestDto.AppointmentDate, CorporateCustomerId = HttpContext.User.GetUserId(), TransportRequestId = requestDto.TransportRequestId, Price = requestDto.Price, CrewId = requestDto.CrewId });
        return Ok(result);
    }

    [HttpPost("CancelOffer")]
    public async Task<IActionResult> CancelOffer(CancelOrAcceptOfferRequestDto requestDto)
    {
        var result = await _mediator.Send(new CancelOfferCommand() { OfferId = requestDto.OfferId, UserId = HttpContext.User.GetUserId() });
        return Ok(result);
    }

    [HttpPost("AcceptOffer")]
    public async Task<IActionResult> AcceptOffer(CancelOrAcceptOfferRequestDto requestDto)
    {
        var result = await _mediator.Send(new AcceptOfferCommand() { OfferId = requestDto.OfferId, UserId = HttpContext.User.GetUserId() });
        return Ok(result);
    }

    [HttpGet("GetAllAcceptedOfferByUserId")]
    public async Task<IActionResult> GetAllAcceptedOfferByUserId()
    {
        var result = await _mediator.Send(new GetAllAcceptedOfferByUserIdQuery() { UserId = HttpContext.User.GetUserId() });
        return Ok(result);
    }
    
    [HttpGet("GetAllOfferByUserId")]
    public async Task<IActionResult> GetAllOfferByUserId()
    {
        var result = await _mediator.Send(new GetAllOfferByUserIdQuery() { UserId = HttpContext.User.GetUserId() });
        return Ok(result);
    }

    [HttpGet("{offerId:int}")]
    public async Task<ActionResult<OfferDto>> GetOfferById([FromRoute] int offerId)
    {
        var result = await _mediator.Send(new GetAcceptedOfferByOfferIdQuery()
        {
            OfferId = offerId,
            UserId = HttpContext.User.GetUserId()
        });
        return Ok(result);
    }

}