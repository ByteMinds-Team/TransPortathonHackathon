using Application.Common.Behaviours.Authentication;
using Application.Common.Behaviours.Authorization;
using Application.Constants;
using Application.Features.Employees.Dtos;
using MediatR;

namespace Application.Features.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeCommand :IRequest<DeletedEmployeeDto> , ISecuredRequest
{
    public int EmployeeId { get; set; }
    public int CorporateCustomerId { get; set; }

    public string[] Roles => new[] { PermissionConstant.Corporate }; 
}