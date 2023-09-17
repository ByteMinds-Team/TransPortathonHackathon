using Domain.Entities.Common;

namespace Domain;

public class Vehicle : Entity
{
    public string NumberPlate { get; set; }
    public string Brand { get; set; }
    public string Color { get; set; }
    public int ModelYear { get; set; }
    public string ImagePath { get; set; }
    public int CorporateCustomerId { get; set; }
    public virtual CorporateCustomer CorporateCustomer { get; set; }
    public virtual ICollection<Offer> Offers { get; set; }

    //public virtual ICollection<Driver> Drivers { get; set; }
}   
