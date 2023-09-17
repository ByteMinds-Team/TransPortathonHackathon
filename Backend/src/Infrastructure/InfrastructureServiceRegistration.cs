using Application.Services;
using Infrastructure.Security;
using Infrastructure.Utilities.CloudinaryAdapter;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<ITokenHelper, JwtHelper>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IImageService, CloudinaryAdapter>();
        return services;
    }
}