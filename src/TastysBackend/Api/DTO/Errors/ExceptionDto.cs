using System.Text.Json.Serialization;

namespace Tastys.API;

/// <summary>
/// Representa una respuesta de excepción.
/// </summary>
public record ExceptionDto : ErrorResponseDto
{
    /// <summary>
    /// Excepción encapsulada.
    /// </summary>
    [JsonIgnore]
    public required Exception Exception { get; init; }

    /// <summary>
    /// Contiene el mensaje de error de la excepción.
    /// </summary>
    [JsonInclude]
    public string ExceptionDetails => Exception.Message;

    /// <summary>
    /// Contiene la pila de llamadas hasta el punto en que se produjo la excepción.
    /// </summary>
    [JsonInclude]
    public string ExceptionStackTrace => Exception.StackTrace ?? "Pila vacía.";
}
