using Application;
using Application.Repositories.EntityFramework;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Persistence.Configurations;
using Persistence.Repositories.EntityFramework;
using Persistence.Repositories.EntityFramework.Contexts;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IMongoDatabase>(options =>
        {
            var settings = configuration.GetSection("MongoDBSettings").Get<MongoSettings>();
            var client = new MongoClient(settings.ConnectionString);
            return client.GetDatabase(settings.CollectionName);
        });

        services.AddDbContext<BaseDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("templatesql")));

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<ICorporateUserRepository, CorporateUserRepository>();
        services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<IDriverRepository, DriverRepository>();
        services.AddScoped<ITransportTypeRepository, TransportTypeRepository>();
        services.AddScoped<IDateGapRepository, DateGapRepository>();
        services.AddScoped<ITransportRequestRepository, TransportRequestRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IOfferRepository, OfferRepository>();
        services.AddScoped<ICrewRepository, CrewRepository>();

        return services;
    }
}