using Domain;

namespace Application;

public class CorporateCustomerInformation
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string ProfileImagePath { get; set; }
    public string Email { get; set; }
    public string CompanyName { get; set; }
    public List<Vehicle> Vehicles { get; set; }
    public List<Employee> Employees { get; set; }
    public List<Crew> Crews { get; set; }
}
