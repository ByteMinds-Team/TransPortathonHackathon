using Domain.Entities.Common;

namespace Domain;

public class Offer : Entity
{
    public double Price { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Description { get; set; }
    public int CorporateCustomerId { get; set; }
    public int TransportRequestId { get; set; }
    public int CrewId { get; set; }
    public bool IsAccepted { get; set; } = false;
    public bool IsCanceled { get; set; } = false;
    public virtual CorporateCustomer CorporateCustomer { get; set; }
    public virtual TransportRequest TransportRequest { get; set; }
    public virtual Crew Crew { get; set; }
    public virtual ICollection<Vehicle> Vehicles { get; set; }
}
