using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tastys.Domain;

public partial class Usuario
{
    [Key]
    public int UsuarioID { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string Email { get; set; } = null!;

    [Required]
    [MaxLength(200)]
    public string Auth0Id { get; set; } = null!;

    public bool IsDeleted { get; set; }

    [Column(TypeName = "datetime")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime? create_at { get; set; } = DateTime.UtcNow;

    public virtual ICollection<Receta> Recetas { get; set; } = new List<Receta>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
