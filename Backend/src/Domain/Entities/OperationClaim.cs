using Domain.Entities.Common;

namespace Domain.Entities;

public class OperationClaim : Entity
{
    public string Name { get; set; }

    public OperationClaim()
    {
    }

    public OperationClaim(int id, string name) : base(id)
    {
        Name = name;
    }
}