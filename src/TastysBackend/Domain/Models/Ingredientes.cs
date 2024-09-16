using System.ComponentModel.DataAnnotations;

namespace Tastys.Domain;

public partial class Ingredientes
{
    [Key]
    public int IngredientesID { get; set; }
    [Required]
    public string Name { get; set; }
}