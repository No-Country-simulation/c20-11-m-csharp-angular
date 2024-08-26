using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public partial class Review
{
    [Key]
    public int ReviewID { get; set; }

    [ForeignKey("Usuario")]
    public int UsuarioID { get; set; }

    [ForeignKey("Receta")]
    public int RecetaID { get; set; }

    [Column(TypeName = "text")]
    public string? Comentario { get; set; }

    public int Calificacion { get; set; }

    public bool IsDeleted { get; set; }

    [Column(TypeName = "datetime")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime? create_at { get; set; } = DateTime.UtcNow;

    public virtual Receta Receta { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
