using Application.Common.Behaviours.Authentication;
using Application.Common.Behaviours.Authorization;
using Application.Constants;
using Application.Features.Employees.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommand : IRequest<CreatedEmployeeDto>,ISecuredRequest
{
    public string[] Roles => new[] {PermissionConstant.Corporate };

    public string FullName { get; set; }
    public IFormFile ProfilePhoto { get; set; }
    public int CorporateCustomerId { get; set; }
}