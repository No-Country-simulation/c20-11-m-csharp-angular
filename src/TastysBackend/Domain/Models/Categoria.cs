using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public partial class Categoria
{
    [Key]
    public int CategoriaID { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = null!;

    public virtual ICollection<Receta> Recetas { get; set; } = new List<Receta>();
}
