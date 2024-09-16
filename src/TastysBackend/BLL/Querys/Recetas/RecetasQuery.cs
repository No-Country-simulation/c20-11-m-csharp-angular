using System.ComponentModel.DataAnnotations;

namespace Tastys.BLL;

/// <summary>
/// Representa los filtros que se pueden agregar a la búsqueda de recetas.
/// </summary>
public record RecetasQuery : PaginatedQuery
{
    /// <summary>
    /// Parámetro que indica el fragmento de texto que la receta debe incluir.
    /// </summary>
    [MaxLength(150)]
    public string? S { get; init; }

    /// <summary>
    /// La cantidad máxima de reviews a incluir en cada receta.
    /// Debe ser mayor que o igual a 0.
    /// </summary>
    [Range(0, int.MaxValue)]
    public int CantReviews { get; init; } = 1;
}
