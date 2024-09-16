using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Tastys.Domain;
public partial class Ingrediente
{
    [Key]
    public int IngredienteID { get; set; } // Cambiado de IngredientesID a IngredienteID para consistencia
    [Required]
    public string Nombre { get; set; }
    public string Cantidad { get; set; }
    public ICollection<RecetaIngrediente> RecetaIngredientes { get; set; } = new List<RecetaIngrediente>();
    [JsonIgnore]
    public ICollection<Receta> Recetas { get; set; } = new List<Receta>();
}

