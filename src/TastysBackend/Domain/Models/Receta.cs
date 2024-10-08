﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Tastys.Domain;
public partial class Receta
{
    [Key]
    public int RecetaID { get; set; }
    [Required]
    public int UsuarioID { get; set; }
    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = null!;
    [Required]
    [Column(TypeName = "text")]
    public string Descripcion { get; set; } = null!;
    [Required]
    [Column(TypeName = "text")]
    public string ImageUrl { get; set; } = null!;
    [Required]
    public string? TiempoCoccion { get; set; } = null;
    [JsonIgnore]
    public ICollection<Ingrediente> Ingredientes { get; set; } = new List<Ingrediente>();
    [JsonIgnore]
    public ICollection<RecetaIngrediente> RecetaIngredientes { get; set; } = new List<RecetaIngrediente>();
    public bool IsDeleted { get; set; } = false;
    [Column(TypeName = "datetime")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime? create_at { get; set; } = DateTime.UtcNow;
    [JsonIgnore]
    public virtual Usuario? Usuario { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Review>? Reviews { get; set; } = new List<Review>();
    [JsonIgnore]
    public virtual ICollection<Categoria>? Categorias { get; set; } = new List<Categoria>();
    [JsonIgnore]
    public virtual ICollection<RecetaCategoria> RecetaCategorias { get; set; } = new List<RecetaCategoria>();
}

