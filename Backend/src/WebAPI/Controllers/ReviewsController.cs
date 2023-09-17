using Application.Features.Reviews.Commands.CreateReview;
using Application.Features.Reviews.Dtos;
using Application.Features.Reviews.Queries.GetAllByCorporateCustomerId;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReviewsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("CreateReview")]
    public async Task<IActionResult> CreateReview([FromBody]ReviewCreateRequestDto requestDto)
    {
        var result = await _mediator.Send(new CreateReviewCommand(){Comment = requestDto.Comment,OfferId = requestDto.OfferId,Point = requestDto.Point,UserId = HttpContext.User.GetUserId()});
        return Ok(result);
    }
    
    [HttpGet("GetAllReviewByCorporateUserId")]
    public async Task<IActionResult> GetAllReviewByCorporateUserId([FromQuery]GetAllReviewByCorporateCustomerIdCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}