using Application.Common.Behaviours.Authentication;
using Application.Common.Behaviours.Authorization;
using Application.Constants;
using Application.Features.Employees.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Employees.Commands.UpdateEployee;

public class UpdateEployeeCommand : IRequest<UpdatedEmployeeDto> , ISecuredRequest
{
    public int EmployeeId { get; set; }

    public string FullName { get; set; }
    public int CorporateCustomerId { get; set; }
    public IFormFile ProfilePhoto { get; set; }

    public string[] Roles => new[] { PermissionConstant.Corporate };
}