using Domain.Entities;
using Domain.Entities.Common;

namespace Domain;

public class Message : Entity
{
    public string Text { get; set; }
    public int ReceiverId { get; set; }
    public int SenderId { get; set; }
    public virtual User Receiver { get; set; }
    public virtual User Sender { get; set; }
}
