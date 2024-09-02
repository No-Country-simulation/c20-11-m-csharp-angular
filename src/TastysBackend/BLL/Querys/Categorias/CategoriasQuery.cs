using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
    /// Indica si se ordenan las categorias según la cantidad total de recetas.
    /// </summary>
    public Ordenamiento OrdenPorCantRecetas { get; init; } = Ordenamiento.Ninguno;
}
