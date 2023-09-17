using Domain.Entities.Common;

namespace Domain;

public class Employee : Entity
{
    public string FullName { get; set; }
    public string ProfileImagePath { get; set; }
    public int CorporateCustomerId { get; set; }
    public virtual CorporateCustomer CorporateCustomer { get; set; }
    public virtual ICollection<Driver> Drivers { get; set; }
    public virtual ICollection<Crew> Crews { get; set; }
}
