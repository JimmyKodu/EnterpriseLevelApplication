using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using EnterpriseApp.Infrastructure.Data;
using EnterpriseApp.Infrastructure.Repositories;
using EnterpriseApp.Core.Interfaces.Repositories;
using EnterpriseApp.Infrastructure.Caching;
using StackExchange.Redis;
using Serilog;

namespace EnterpriseApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString, string? redisConnection = null)
    {
        // Add DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString,
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        // Add Repositories
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Add Redis if connection string is provided
        if (!string.IsNullOrEmpty(redisConnection))
        {
            var multiplexer = ConnectionMultiplexer.Connect(redisConnection);
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);
            services.AddSingleton<ICacheService, RedisCacheService>();
        }

        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        return services;
    }
}
