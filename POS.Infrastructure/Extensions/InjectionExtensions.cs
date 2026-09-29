using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POS.Infrastructure.Persistences.Contexts;
using POS.Infrastructure.Persistences.Interfaces;
using POS.Infrastructure.Persistences.Repositories;

namespace POS.Infrastructure.Extensions;

public static class InjectionExtensions
{
    public static IServiceCollection AddInjecionInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Add your infrastructure services here
        // For example, you can add DbContext, repositories, etc.
        // Example: Adding DbContext
        services.AddDbContext<PosContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("POSConnection")), ServiceLifetime.Transient);

        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        return services;
    }
}
