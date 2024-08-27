using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    public bool IsDeleted { get; set; }
    [Column(TypeName = "datetime")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime? create_at { get; set; } = DateTime.UtcNow;

    public virtual Usuario Usuario { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Categoria> Categorias { get; set; } = new List<Categoria>();
}
