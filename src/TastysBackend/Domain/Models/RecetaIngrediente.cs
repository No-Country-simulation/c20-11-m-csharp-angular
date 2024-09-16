using System.ComponentModel.DataAnnotations;

namespace Tastys.Domain;

public partial class RecetaIngrediente
{
    [Key]
    public int RecetaIngredienteId { get; set; }
    public int RecetaID { get; set; }
    public int IngredienteId { get; set; }
    public virtual Ingrediente Ingrediente { get; set; }= null!;
    public virtual Receta Receta { get; set; }= null!;
}
