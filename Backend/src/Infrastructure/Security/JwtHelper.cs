using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Application.Features.Auth.Models;
using Application.Services;
using Domain.Entities;
using Infrastructure.Extensions;
using Infrastructure.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Security;

public class JwtHelper : ITokenHelper
{
    public IConfiguration Configuration { get; }
    private readonly TokenOptions _tokenOptions;
    private DateTime _accessTokenExpiration;

    public JwtHelper(IConfiguration configuration)
    {
        Configuration = configuration;
        _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
    }

    public AccessToken CreateToken(User user, IList<OperationClaim> operationClaims)
    {
        _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        SigningCredentials signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        JwtSecurityToken jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        string? token = jwtSecurityTokenHandler.WriteToken(jwt);

        return new AccessToken
        {
            Token = token,
            Expiration = _accessTokenExpiration
        };
    }

    private string CreateRefreshToken()
    {
        var number = new byte[32];
        using (var random = RandomNumberGenerator.Create())
        {
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }

    public RefreshToken CreateRefreshToken(User user, string ipAddress)
    {
        RefreshToken refreshToken = new()
        {
            Client = Guid.NewGuid().ToString(),
            IpAddress = ipAddress,
            RefreshTokenValue = CreateRefreshToken(),
            UserId = user.Id,
            Token = CreateToken(user, user.UserOperationClaims.Select(p => p.OperationClaim).ToList()).Token,
            TokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration)
        };

        return refreshToken;
    }

    public RefreshToken CreateRefreshTokenWithStillClient(User user, string ipAddress,string clientId)
    {
        RefreshToken refreshToken = new()
        {
            Client = clientId,
            IpAddress = ipAddress,
            RefreshTokenValue = CreateRefreshToken(),
            UserId = user.Id,
            Token = CreateToken(user, user.UserOperationClaims.Select(p => p.OperationClaim).ToList()).Token,
            TokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration)

        };

        return refreshToken;
    }



    public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
                                                   SigningCredentials signingCredentials,
                                                   IList<OperationClaim> operationClaims)
    {
        JwtSecurityToken jwt = new(
            tokenOptions.Issuer,
            tokenOptions.Audience,
            expires: _accessTokenExpiration,
            notBefore: DateTime.Now,
            claims: SetClaims(user, operationClaims),
            signingCredentials: signingCredentials
        );
        return jwt;
    }

    private IEnumerable<Claim> SetClaims(User user, IList<OperationClaim> operationClaims)
    {
        List<Claim> claims = new();
        claims.AddNameIdentifier(user.Id.ToString());
        claims.AddEmail(user.Email);
        claims.AddName($"{user.FullName}");
        claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());
        return claims;
    }
}