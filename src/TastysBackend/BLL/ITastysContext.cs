using Microsoft.EntityFrameworkCore;
using Tastys.Domain;

namespace Tastys.BLL;
public interface ITastysContext
{
    DbSet<Categoria> Categorias { get; set; }

    DbSet<RecetaCategoria> RecetaCategoria { get; set; }

    DbSet<Receta> Recetas { get; set; }

    DbSet<Review> Reviews { get; set; }

    DbSet<Usuario> Usuarios { get; set; }

    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}