using Microsoft.AspNetCore.Http;

namespace Application.Features.Employees.Dtos;

public class EmployeeRequestDto
{

    public string FullName { get; set; }

    public IFormFile ProfilePhoto { get; set; } 
    
    
}