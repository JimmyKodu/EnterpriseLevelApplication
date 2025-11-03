using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System.Reflection;

namespace EnterpriseApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // Add MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        // Add AutoMapper
        services.AddAutoMapper(assembly);

        // Add FluentValidation
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
