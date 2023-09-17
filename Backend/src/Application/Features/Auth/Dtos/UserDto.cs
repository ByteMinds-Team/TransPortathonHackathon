using Domain.Entities;

namespace Application.Features.Auth;

public class UserDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string ImagePath { get; set; }
    public List<OperationClaim> OperationClaims { get; set; }
}