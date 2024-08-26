using System;
using System.Collections.Generic;

namespace Domain;

public partial class Usuario
{
    public int UsuarioID { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Auth0Id { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime? create_at { get; set; }

    public virtual ICollection<Receta> Recetas { get; set; } = new List<Receta>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
