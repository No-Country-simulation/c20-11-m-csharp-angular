using System;
using System.Collections.Generic;

namespace Domain;

public partial class Review
{
    public int ReviewID { get; set; }

    public int UsuarioID { get; set; }

    public int RecetaID { get; set; }

    public string? Comentario { get; set; }

    public int Calificacion { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? create_at { get; set; }

    public virtual Receta Receta { get; set; } = null!;

    public virtual Usuarios Usuario { get; set; } = null!;
}
