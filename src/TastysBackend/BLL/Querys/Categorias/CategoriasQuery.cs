using System.ComponentModel.DataAnnotations;

namespace Tastys.BLL;

/// <summary>
/// Representa los filtros que se pueden agregar cuando se piden todas las categorias.
/// </summary>
public record CategoriasQuery : PaginatedQuery
{
    /// <summary>
    /// Cantidad de recetas a incluir con cada categoria.
    /// </summary>
    [Range(0, int.MaxValue)]
    public int? CantRecetas { get; init; } = 3;

    /// <summary>
    /// Indica si se ordenan de las categorias según la cantidad total de recetas.
    /// </summary>
    public Ordering OrdenPorCantRecetas { get; init; } = Ordering.None;
}
