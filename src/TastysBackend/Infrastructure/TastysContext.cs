using System.Collections.Generic;
using Tastys.BLL;
using Tastys.Domain;
using Microsoft.EntityFrameworkCore;

namespace Tastys.Infrastructure;

public partial class TastysContext : DbContext, ITastysContext
{
    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<RecetaCategoria> RecetaCategoria { get; set; }
    public virtual DbSet<Receta> Recetas { get; set; }
    public virtual DbSet<Review> Reviews { get; set; }
    public virtual DbSet<Categoria> Categorias { get; set; }

    public TastysContext(DbContextOptions<TastysContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Review>()
            .HasOne(d => d.Receta)
            .WithMany(p => p.Reviews)
            .HasForeignKey(d => d.RecetaID);

        modelBuilder.Entity<Review>()
            .HasOne(d => d.Usuario)
            .WithMany(p => p.Reviews)
            .HasForeignKey(d => d.UsuarioID);

        modelBuilder.Entity<RecetaCategoria>()
            .HasKey(rc => new { rc.RecetaID, rc.CategoriaID });

        modelBuilder.Entity<RecetaCategoria>()
            .HasOne(rc => rc.Receta)
            .WithMany(r => r.RecetaCategorias)
            .HasForeignKey(rc => rc.RecetaID);

        modelBuilder.Entity<RecetaCategoria>()
            .HasOne(rc => rc.Categoria)
            .WithMany(c => c.RecetaCategorias)
            .HasForeignKey(rc => rc.CategoriaID);

        modelBuilder.Entity<Receta>()
            .HasMany(r => r.Categorias)
            .WithMany(c => c.Recetas)
            .UsingEntity<RecetaCategoria>(
                j => j
                    .HasOne(rc => rc.Categoria)
                    .WithMany(c => c.RecetaCategorias)
                    .HasForeignKey(rc => rc.CategoriaID),
                j => j
                    .HasOne(rc => rc.Receta)
                    .WithMany(r => r.RecetaCategorias)
                    .HasForeignKey(rc => rc.RecetaID),
                j =>
                {
                    j.HasKey(rc => new { rc.RecetaID, rc.CategoriaID });
                });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
