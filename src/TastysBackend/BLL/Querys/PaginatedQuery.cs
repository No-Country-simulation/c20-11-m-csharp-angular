using System.ComponentModel.DataAnnotations;

namespace Tastys.BLL;

/// <summary>
/// Representa una query para una petición que muestra sus resultados de manera paginada.
/// </summary>
public record PaginatedQuery
{
    /// <summary>
    /// El número de página a traer (la primera es la 0).
    /// Debe ser mayor que 1.
    /// </summary>
    [Range(0, int.MaxValue)]
    public int? Page { get; set; } = 0;

    /// <summary>
    /// El tamaño de las páginas.
    /// Debe ser mayor que 1.
    /// </summary>
    [Range(1, int.MaxValue)]
    public int? PageSize { get; set; } = 10;
}
