using Domain.Entities.Common;
using Domain.Enums;

namespace Domain.Entities;

public class User : Entity
{
    public string FullName { get; set; }

    public string ProfileImagePath { get; set; }
    public string Email { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }
    public bool Status { get; set; }
    //public AuthenticatorType AuthenticatorType { get; set; }

    public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; }

    public User()
    {
        UserOperationClaims = new HashSet<UserOperationClaim>();
        RefreshTokens = new HashSet<RefreshToken>();
    }

    public User(int id, string fullName, string email,string profileImagePath, byte[] passwordSalt, byte[] passwordHash,
        bool status) : this()
    {
        Id = id;
        FullName = fullName;
        ProfileImagePath = profileImagePath;
        Email = email;
        PasswordSalt = passwordSalt;
        PasswordHash = passwordHash;
        Status = status;
    }
}