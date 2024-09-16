﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tastys.Infrastructure;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(TastysContext))]
    partial class TastysContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Tastys.Domain.Categoria", b =>
                {
                    b.Property<int>("CategoriaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("CategoriaID"));

                    b.Property<string>("ImgUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("CategoriaID");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("Tastys.Domain.Ingrediente", b =>
                {
                    b.Property<int>("IngredienteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IngredienteID"));

                    b.Property<string>("Cantidad")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("IngredienteID");

                    b.ToTable("Ingredientes");
                });

            modelBuilder.Entity("Tastys.Domain.Receta", b =>
                {
                    b.Property<int>("RecetaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("RecetaID"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("TiempoCoccion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UsuarioID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("create_at")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<DateTime?>("create_at"));

                    b.HasKey("RecetaID");

                    b.HasIndex("UsuarioID");

                    b.ToTable("Recetas");
                });

            modelBuilder.Entity("Tastys.Domain.RecetaCategoria", b =>
                {
                    b.Property<int>("RecetaID")
                        .HasColumnType("int");

                    b.Property<int>("CategoriaID")
                        .HasColumnType("int");

                    b.HasKey("RecetaID", "CategoriaID");

                    b.HasIndex("CategoriaID");

                    b.ToTable("RecetaCategoria");
                });

            modelBuilder.Entity("Tastys.Domain.RecetaIngrediente", b =>
                {
                    b.Property<int>("RecetaID")
                        .HasColumnType("int");

                    b.Property<int>("IngredienteId")
                        .HasColumnType("int");

                    b.Property<int>("RecetaIngredienteId")
                        .HasColumnType("int");

                    b.HasKey("RecetaID", "IngredienteId");

                    b.HasIndex("IngredienteId");

                    b.ToTable("RecetaIngredientes");
                });

            modelBuilder.Entity("Tastys.Domain.Review", b =>
                {
                    b.Property<int>("ReviewID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ReviewID"));

                    b.Property<int>("Calificacion")
                        .HasColumnType("int");

                    b.Property<string>("Comentario")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("RecetaID")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("create_at")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<DateTime?>("create_at"));

                    b.HasKey("ReviewID");

                    b.HasIndex("RecetaID");

                    b.HasIndex("UsuarioID");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Tastys.Domain.Usuario", b =>
                {
                    b.Property<int>("UsuarioID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("UsuarioID"));

                    b.Property<string>("Auth0Id")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("create_at")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<DateTime?>("create_at"));

                    b.HasKey("UsuarioID");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Tastys.Domain.Receta", b =>
                {
                    b.HasOne("Tastys.Domain.Usuario", "Usuario")
                        .WithMany("Recetas")
                        .HasForeignKey("UsuarioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Tastys.Domain.RecetaCategoria", b =>
                {
                    b.HasOne("Tastys.Domain.Categoria", "Categoria")
                        .WithMany("RecetaCategorias")
                        .HasForeignKey("CategoriaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tastys.Domain.Receta", "Receta")
                        .WithMany("RecetaCategorias")
                        .HasForeignKey("RecetaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Receta");
                });

            modelBuilder.Entity("Tastys.Domain.RecetaIngrediente", b =>
                {
                    b.HasOne("Tastys.Domain.Ingrediente", "Ingrediente")
                        .WithMany("RecetaIngredientes")
                        .HasForeignKey("IngredienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tastys.Domain.Receta", "Receta")
                        .WithMany("RecetaIngredientes")
                        .HasForeignKey("RecetaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingrediente");

                    b.Navigation("Receta");
                });

            modelBuilder.Entity("Tastys.Domain.Review", b =>
                {
                    b.HasOne("Tastys.Domain.Receta", "Receta")
                        .WithMany("Reviews")
                        .HasForeignKey("RecetaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tastys.Domain.Usuario", "Usuario")
                        .WithMany("Reviews")
                        .HasForeignKey("UsuarioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receta");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Tastys.Domain.Categoria", b =>
                {
                    b.Navigation("RecetaCategorias");
                });

            modelBuilder.Entity("Tastys.Domain.Ingrediente", b =>
                {
                    b.Navigation("RecetaIngredientes");
                });

            modelBuilder.Entity("Tastys.Domain.Receta", b =>
                {
                    b.Navigation("RecetaCategorias");

                    b.Navigation("RecetaIngredientes");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Tastys.Domain.Usuario", b =>
                {
                    b.Navigation("Recetas");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
