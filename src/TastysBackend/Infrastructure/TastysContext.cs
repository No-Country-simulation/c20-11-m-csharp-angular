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
        modelBuilder.Entity<Receta>()
            .HasOne(d => d.Usuario)
            .WithMany(p => p.Recetas)
            .HasForeignKey(d => d.UsuarioID);

        modelBuilder.Entity<Review>()
            .HasOne(d => d.Receta)
            .WithMany(p => p.Reviews)
            .HasForeignKey(d => d.RecetaID);

        modelBuilder.Entity<Review>()
            .HasOne(d => d.Usuario)
            .WithMany(p => p.Reviews)
            .HasForeignKey(d => d.UsuarioID);

        modelBuilder.Entity<Receta>()
            .HasMany(d => d.Categorias)
            .WithMany(p => p.Recetas)
            .UsingEntity<RecetaCategoria>();
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
