using Tastys.Domain;

namespace Tastys.BLL;

public class RecetaDto
{
    /// <summary>
    /// El id de la receta.
    /// </summary>
    public int RecetaID { get; set; }

    /// <summary>
    /// El título de la receta.
    /// </summary>
    public string Nombre { get; set; }

    /// <summary>
    /// El contenido de la receta.
    /// </summary>
    public string Descripcion { get; set; }

    /// <summary>
    /// La imágen miniatura de la receta.
    /// </summary>
    public string ImageUrl { get; set; }

    /// <summary>
    /// El puntaje promedio de la receta.
    /// </summary>
    public float Puntuacion { get; set; }

    /// <summary>
    /// El usuario creador de la receta.
    /// </summary>
    public UsuarioPublicDto Usuario { get; set; }

    /// <summary>
    /// Reviews de la receta.
    /// </summary>
    public ICollection<ReviewDto> Reviews { get; set; }

    /// <summary>
    /// Las categorías de la receta.
    /// </summary>
    public ICollection<CategoriaDto> Categorias { get; set; }

    /// <summary>
    /// Los ingredientes de la receta.
    /// </summary>
    public ICollection<IngredienteDto> Ingredientes { get; set; }

    /// <summary>
    /// El tiempo de cocción de la receta.
    /// </summary>
    public string? TiempoCoccion { get; set; }

    /// <summary>
    /// Si es true, la receta está por ser eliminada.
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Fecha de creación de la receta.
    /// </summary>
    public DateTime? CreateAt { get; set; } = DateTime.Now;
}
