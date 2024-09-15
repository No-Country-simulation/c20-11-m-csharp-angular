namespace Tastys.API;

/// <summary>
/// Representa una respuesta de error de validación.
/// </summary>
public record ValidationErrorDto : ErrorResponseDto
{
    /// <summary>
    /// El nombre del parámetro inválido.
    /// </summary>
    public string? Parameter { get; init; }
}
