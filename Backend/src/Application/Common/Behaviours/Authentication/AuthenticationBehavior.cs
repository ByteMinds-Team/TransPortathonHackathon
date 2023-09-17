using System.Security.Authentication;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Behaviours.Authentication;

public class AuthenticationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, IAuthRequest
{

    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticationBehavior(IHttpContextAccessor httpContextAccessor)
        => (_httpContextAccessor) = (httpContextAccessor);

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated) throw new AuthenticationException("you are not Authentication");

        TResponse response = await next();
        return response;
    }

    //public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    //{
    //    if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated) throw new AuthenticationException("you are not Authentication");

    //    TResponse response = await next();
    //    return response;
    //}


}