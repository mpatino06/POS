using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POS.Application.Interfaces;
using POS.Application.Services;
using System.Reflection;

namespace POS.Application.Extensions;

public static class InjectionExtensions
{
    public static IServiceCollection AddInjectionApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConfiguration>(configuration);

        var assemblies = AppDomain.CurrentDomain
            .GetAssemblies()
            .Where(a => !a.IsDynamic)
            .ToArray();

        // Register FluentValidation validators from assemblies and enable automatic validation
        services.AddValidatorsFromAssemblies(assemblies);
        //services.AddFluentValidationAutoValidation();

        // Register AutoMapper with the executing assembly
        services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());

        // Register application services
        services.AddScoped<ICategoryApplication, CategoryApplication>();

        return services;
    }
}
