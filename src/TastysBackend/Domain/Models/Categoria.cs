using System;
using System.Collections.Generic;

namespace Domain;

public partial class Categoria
{
    public int CategoriaID { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Receta> Receta { get; set; } = new List<Receta>();
}
