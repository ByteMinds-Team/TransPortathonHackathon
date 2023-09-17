using System.Reflection;
using Application.Common.Behaviours.Authorization;
using Application.Common.Behaviours.Validation;
using Application.Features.Messages;
using Application.Features.TransportType.Rules;
using Application.Features.Vehicles.Rules;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
        services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        services.AddScoped<VehicleBusinessRules>();
        services.AddScoped<TransportTypeBusinessRules>();
        services.AddScoped<MessageBusinessRules>();
        
        return services;
    }
}