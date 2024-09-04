using System.ComponentModel.DataAnnotations;

namespace Tastys.BLL;

/// <summary>
/// Representa una query para una petición que muestra sus resultados de manera paginada.
/// </summary>
public record PaginatedQuery
{
    /// <summary>
    /// La cantidad de elementos a saltear en la paginación.
    /// Debe ser mayor que 0.
    /// </summary>
    [Range(0, int.MaxValue)]
    public int? Offset { get; init; }

    /// <summary>
    /// La cantidad de elementos a incluir en la paginación.
    /// Debe ser mayor que 0.
    /// </summary>
    [Range(0, int.MaxValue)]
    public int? Length { get; init; }
}
