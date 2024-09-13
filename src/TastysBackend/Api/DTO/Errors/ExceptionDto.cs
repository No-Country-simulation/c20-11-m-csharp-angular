namespace Tastys.API;

/// <summary>
/// Representa una respuesta de excepción.
/// </summary>
public record ExceptionDto : ErrorResponseDto
{
    /// <summary>
    /// Excepción encapsulada.
    /// </summary>
    public required Exception Exception { get; init; }
}
