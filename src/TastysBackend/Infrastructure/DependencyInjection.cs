using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Tastys.BLL;
using Tastys.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TastysContext>(
            options => options
                .UseMySql(
                    configuration.GetConnectionString("MySqlConnection"),
                    new MySqlServerVersion(new Version(8, 0, 23)),
                    options =>
                    {
                        options.MigrationsAssembly("Infrastructure");
                        options.EnableStringComparisonTranslations();
                    })
                );

        services.AddScoped<ITastysContext>(provider => provider.GetRequiredService<TastysContext>());

        services.AddScoped<TastysContextInitialiser>();

        return services;
    }
}
