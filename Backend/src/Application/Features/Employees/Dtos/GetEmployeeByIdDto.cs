namespace Application.Features.Employees.Dtos;

public class GetEmployeeByIdDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string ProfileImagePath { get; set; }
}