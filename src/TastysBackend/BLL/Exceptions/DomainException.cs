namespace Tastys.BLL;

/// <summary>
/// Representa una violación a las reglas de la capa de dominio.
/// </summary>
[Serializable]
public abstract class DomainException : Exception
{
    /// <summary>
    /// Crea una nueva instancia de este error sin detalles.
    /// </summary>
    public DomainException() { }

    /// <summary>
    /// Crea una nueva instancia de este error con un mensaje específico.
    /// </summary>
    /// <param name="message">Mensaje que describe el error.</param>
    public DomainException(string message) : base(message) { }

    /// <summary>
    /// Crea una nueva instancia de este error con un mensaje específico y una referencia a la excepción original.
    /// </summary>
    /// <param name="message">Mensaje que describe el error.</param>
    /// <param name="inner">Excepción causante de la excepción actual.</param>
    public DomainException(string message, Exception inner) : base(message, inner) { }
}