using Microsoft.AspNetCore.Http;

namespace Application.Features.Employees.Commands.UpdateEployee;

public class UpdateEmployeeUpdatePhotoRequestDto
{
    public string EmployeeId { get; set; }
    public IFormFile ProfilePhoto { get; set; }
}