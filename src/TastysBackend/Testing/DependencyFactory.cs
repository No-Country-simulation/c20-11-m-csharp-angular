using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tastys.BLL;
using Tastys.Infrastructure;

namespace Tastys.Testing;

internal static class DependencyFactory
{
    public static TastysContext CreateInMemoryContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<TastysContext>();
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

        var dbContext = new TastysContext(optionsBuilder.Options);
        dbContext.Database.EnsureCreated();

        return dbContext;
    }

    public static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        var mapper = new Mapper(config);

        return mapper;
    }
}
