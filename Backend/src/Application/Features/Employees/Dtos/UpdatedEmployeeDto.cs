namespace Application.Features.Employees.Dtos;

public class UpdatedEmployeeDto
{
    public int EmployeeId { get; set; }

    public string FullName { get; set; }
    public int CorporateCustomerId { get; set; }
    public string ProfilePhoto { get; set; }
}