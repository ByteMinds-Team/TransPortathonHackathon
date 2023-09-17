using Domain.Entities;
using Domain.Entities.Common;

namespace Domain;

public class Review : Entity
{
    public int Point { get; set; }
    public string Comment { get; set; }
    public int CorporateCustomerId { get; set; }
    public int UserId { get; set; }
    public virtual CorporateCustomer CorporateCustomer { get; set; }
    public virtual User User { get; set; }
}
