using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Tastys.Infrastructure;

public static class InitialiserExtensions
{
    public static void InitialiseDatabase(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<TastysContextInitialiser>();

        initialiser.Initialise();

        initialiser.SeedData();
    }
}

public class TastysContextInitialiser
{
    private readonly ILogger<TastysContextInitialiser> _logger;
    private readonly TastysContext _context;

    public TastysContextInitialiser(ILogger<TastysContextInitialiser> logger, TastysContext context)
    {
        _logger = logger;
        _context = context;
    }

    public void Initialise()
    {
        try
        {
            _context.Database.Migrate();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ocurrió un error migrando la base de datos.");
            throw;
        }
    }

    public void SeedData()
    {
        try
        {
            TestDataSeeder.SeedDataToContext(_context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ocurrió un error seedeando la base de datos.");
            throw;
        }
    }
}