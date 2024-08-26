using System;
using System.Collections.Generic;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Tastys.Infrastructure;

public partial class TastysContext : DbContext
{
    public virtual DbSet<Usuarios> Usuarios { get; set; }

    public virtual DbSet<Receta> Receta { get; set; }

    public virtual DbSet<Review> Review { get; set; }

    public virtual DbSet<Categoria> Categoria { get; set; }


    public TastysContext(DbContextOptions<TastysContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CategoriaID).HasName("PRIMARY");

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Receta>(entity =>
        {
            entity.HasKey(e => e.RecetaID).HasName("PRIMARY");

            entity.HasIndex(e => e.UsuarioID, "UsuarioID");

            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.ImageUrl).HasColumnType("text");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.create_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Receta)
                .HasForeignKey(d => d.UsuarioID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Receta_ibfk_1");

            entity.HasMany(d => d.Categoria).WithMany(p => p.Receta)
                .UsingEntity<Dictionary<string, object>>(
                    "RecetaCategoria",
                    r => r.HasOne<Categoria>().WithMany()
                        .HasForeignKey("CategoriaID")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("RecetaCategoria_ibfk_2"),
                    l => l.HasOne<Receta>().WithMany()
                        .HasForeignKey("RecetaID")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("RecetaCategoria_ibfk_1"),
                    j =>
                    {
                        j.HasKey("RecetaID", "CategoriaID").HasName("PRIMARY");
                        j.HasIndex(new[] { "CategoriaID" }, "CategoriaID");
                    });
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewID).HasName("PRIMARY");

            entity.HasIndex(e => e.RecetaID, "RecetaID");

            entity.HasIndex(e => e.UsuarioID, "UsuarioID");

            entity.Property(e => e.Comentario).HasColumnType("text");
            entity.Property(e => e.create_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Receta).WithMany(p => p.Review)
                .HasForeignKey(d => d.RecetaID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Review_ibfk_1");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Review)
                .HasForeignKey(d => d.UsuarioID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Review_ibfk_2");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.UsuarioID).HasName("PRIMARY");

            entity.HasIndex(e => e.Auth0Id, "Auth0Id").IsUnique();

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.Property(e => e.Auth0Id).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.create_at)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
