namespace Tastys.API;

/// <summary>
/// Representa una respuesta de error genérica.
/// </summary>
public record ErrorResponseDto
{
    /// <summary>
    /// Mensaje que describe el error.
    /// </summary>
    public required string Message { get; init; }
}
