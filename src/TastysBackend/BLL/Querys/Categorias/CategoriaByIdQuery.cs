using System.Text.Json.Serialization;

namespace Tastys.BLL;

/// <summary>
/// Representa los filtros que se pueden agregar cuando se pide una categoría por ID.
/// </summary>
public record CategoriaByIdQuery : PaginatedQuery
{
    /// <summary>
    /// Indica si se ordenan las recetas según la cantidad total de reviews.
    /// </summary>
    public Ordenamiento OrdenPorCantReviews { get; init; } = Ordenamiento.Ninguno;

    /// <summary>
    /// Indica si se ordenan las recetas según su puntuación.
    /// </summary>
    public Ordenamiento OrdenPorPuntuacion { get; init; } = Ordenamiento.Descendente;

    /// <summary>
    /// Indica si se ordenan las recetas según su fecha de publicación.
    /// </summary>
    public Ordenamiento OrdenPorFecha { get; init; } = Ordenamiento.Ninguno;

}
