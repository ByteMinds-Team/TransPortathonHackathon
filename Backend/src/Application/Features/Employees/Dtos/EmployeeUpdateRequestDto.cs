using Microsoft.AspNetCore.Http;

namespace Application.Features.Employees.Dtos;

public class EmployeeUpdateRequestDto
{
    
    public string FullName { get; set; }

    public IFormFile ProfilePhoto { get; set; }
    public int EmployeeId { get; set; }
}