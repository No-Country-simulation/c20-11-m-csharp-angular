using System.ComponentModel.DataAnnotations;

namespace Tastys.BLL;

/// <summary>
/// Representa los filtros que se pueden agregar a la búsqueda de recetas.
/// </summary>
public record RecetasQuery
{
    /// <summary>
    /// Parámetro que indica el fragmento de texto que la receta debe incluir.
    /// </summary>
    [MaxLength(150)]
    public string? S { get; init; }

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

    /// <summary>
    /// La cantidad máxima de reviews a incluir en cada receta.
    /// Debe ser mayor que 0.
    /// </summary>
    [Range(0, int.MaxValue)]
    public int? Reviews_Length { get; init; }
}
