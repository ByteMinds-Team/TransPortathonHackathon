using Application.Features.Employees.Commands.CreateEmployee;
using Application.Features.Employees.Commands.DeleteEmployee;
using Application.Features.Employees.Commands.UpdateEployee;
using Application.Features.Employees.Dtos;
using Application.Features.Employees.Queries.GetAllByCorporateId;
using Application.Features.Employees.Queries.GetAllEmployee;
using Application.Features.Employees.Queries.GetById;
using AutoMapper;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("AddEmployee")]
    public async Task<IActionResult> AddEmployee([FromForm]EmployeeRequestDto dto)
    {
        CreateEmployeeCommand command = new() {FullName = dto.FullName,ProfilePhoto = dto.ProfilePhoto,CorporateCustomerId = HttpContext.User.GetUserId()};
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    [HttpPost("DeleteEmployee")]
    public async Task<IActionResult> DeleteEmployee([FromQuery]int employeeId)
    {
        DeleteEmployeeCommand command = new() {EmployeeId = employeeId, CorporateCustomerId = HttpContext.User.GetUserId()};
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    [HttpPost("UpdateEmployee")]
    public async Task<IActionResult> UpdateEmployee([FromForm]EmployeeUpdateRequestDto dto)
    {
        UpdateEployeeCommand command = new() {FullName = dto.FullName,ProfilePhoto = dto.ProfilePhoto,EmployeeId = dto.EmployeeId,CorporateCustomerId = HttpContext.User.GetUserId()};
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpGet("GetByEmployeeId")]
    public async Task<IActionResult> GetAllEmployee([FromQuery] int employeeId)
    {
        var result = await _mediator.Send(new GetEmployeeByIdQuery() {CorporateCustomerId = HttpContext.User.GetUserId(), EmployeeId = employeeId});
        return Ok(result);
    }

    [HttpGet("GetAllEmployee")]
    public async Task<IActionResult> GetAllEmployee()
    {
        var result = await _mediator.Send(new GetAllEmployeeCommand() {CorporateUserId = HttpContext.User.GetUserId() });
        return Ok(result);
    }

    [HttpGet("GetAllEmployeeByCorporateUser")]
    public async Task<IActionResult> GetAllEmployeeByCorporateUser([FromQuery]int CorporateUserId)
    {
        var result = await _mediator.Send(new GetAllEmployeeByCorporateUserIdCommand() { CorporateUserId = CorporateUserId});
        return Ok(result);
    }
}