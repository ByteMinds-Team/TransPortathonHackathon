namespace Application.Features.Crews.Dtos;

public class CrewCreateRequestDto
{
    public string Name { get; set; }
    public List<int> EmployeeIds { get; set; }
}