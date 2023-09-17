namespace Application.Features.Auth.Models;

public class AccessToken
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}