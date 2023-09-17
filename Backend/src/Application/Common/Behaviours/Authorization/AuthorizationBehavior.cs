using Application.Common.Exceptions;
using Application.Services;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Reflection;

namespace Application.Common.Behaviours.Authorization;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ISecuredRequest
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IIdentityService _identityService;

    public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor, IIdentityService identityService)
        => (_httpContextAccessor, _identityService) = (httpContextAccessor, identityService);

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request.Roles.Length == 0)
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                throw new AuthorizationException("You are not authorized.");

            TResponse res = await next();
            return res;
        }

        bool isNotMatchedARoleClaimWithRequestRoles = _identityService.IsInRole(request.Roles);

        if (isNotMatchedARoleClaimWithRequestRoles) throw new AuthorizationException("You are not authorized.");
        TResponse response = await next();
        return response;
    }
}