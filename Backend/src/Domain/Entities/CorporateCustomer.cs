using Domain.Entities;

namespace Domain;

public class CorporateCustomer : User
{
    public string CompanyName { get; set; }
    public virtual ICollection<Vehicle> Vehicles { get; set; }
    public virtual ICollection<Employee> Employees { get; set; }
    public virtual ICollection<Crew> Crews { get; set; }

}
