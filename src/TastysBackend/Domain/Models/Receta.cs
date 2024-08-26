using System;
using System.Collections.Generic;

namespace Domain;

public partial class Receta
{
    public int RecetaID { get; set; }

    public int UsuarioID { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime? create_at { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Categoria> Categorias { get; set; } = new List<Categoria>();
}
