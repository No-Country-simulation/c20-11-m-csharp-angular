namespace Tastys.BLL;

/// <summary>
/// Representa los filtros que se pueden agregar cuando se pide una categoría por ID.
/// </summary>
public record CategoriaByIdQuery : PaginatedQuery
{
    /// <summary>
    /// Indica si se ordenan las recetas según la cantidad total de reviews.
    /// </summary>
    public Ordering OrdenarPorCantReviews { get; init; } = Ordering.None;

    /// <summary>
    /// Indica si se ordenan las recetas según su puntuación.
    /// </summary>
    public Ordering OrdenarPorPuntuacion { get; init; } = Ordering.Descending;

    /// <summary>
    /// Indica si se ordenan las recetas según su fecha de publicación.
    /// </summary>
    public Ordering OrdenarPorFecha { get; init; } = Ordering.None;

}
