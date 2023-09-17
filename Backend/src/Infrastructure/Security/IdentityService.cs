using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Services;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Security;

public class IdentityService: IIdentityService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IdentityService(IHttpContextAccessor httpContextAccessor)
        => _httpContextAccessor = httpContextAccessor;
    
    public bool IsInRole(string[] roles)
    {
        List<string>? roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();

        if (roleClaims == null) throw new AuthorizationException("Claims not found.");

        bool isNotMatchedARoleClaimWithRequestRoles =
            roleClaims.FirstOrDefault(roleClaim => roles.Any(role => role == roleClaim)).IsNullOrEmpty();

        return isNotMatchedARoleClaimWithRequestRoles;
    }
}