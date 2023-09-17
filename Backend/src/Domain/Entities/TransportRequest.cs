using Domain.Entities;
using Domain.Entities.Common;

namespace Domain;

public class TransportRequest : Entity
{
    public string Description { get; set; }
    public int UserId { get; set; }
    public int DateGapId { get; set; }
    public int TransportTypeId { get; set; }
    public virtual DateGap DateGap { get; set; }
    public virtual User User { get; set; }
    public virtual TransportType TransportType { get; set; }
}
