using Domain.Entities.Common;

namespace Domain.Entities;

public class RefreshToken : Entity
{
    public string RefreshTokenValue { get; set; }
    public string IpAddress { get; set; }
    public string Token { get; set; }
    public DateTime TokenExpiration { get; set; }
    public string Client { get; set; }
    public int UserId { get; set; }



    public virtual User User { get; set; }

    public RefreshToken()
    {
    }

}