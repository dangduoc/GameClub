using Application.Common.Interfaces;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlite(
                     configuration.GetConnectionString("DefaultConnection"),
                          b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                     .MigrationsHistoryTable("__EFMigrationsHistory"))
                     );

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddSingleton(TimeProvider.System);
        return services;
    }

}
