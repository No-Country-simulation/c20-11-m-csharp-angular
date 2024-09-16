using Tastys.BLL;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddBLLServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));

        services.AddScoped<ICategoriaService, CategoriaService>();

        return services;
    }
}
