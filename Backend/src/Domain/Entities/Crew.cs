using Domain.Entities.Common;

namespace Domain;

public class Crew : Entity
{
    public int CorporateCustomerId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Employee> Employees { get; set; }
    public virtual CorporateCustomer CorporateCustomer { get; set; }
}
