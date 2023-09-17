using Domain.Entities.Common;

namespace Domain;

public class Driver : Entity
{
    public int VehicleId { get; set; }
    public int EmployeeId { get; set; }
    public virtual Vehicle Vehicle { get; set; }
    public virtual Employee Employee { get; set; }
}
