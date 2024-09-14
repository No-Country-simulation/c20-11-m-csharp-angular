namespace Tastys.BLL;

/// <summary>
/// Lanzada cuando se recibe un valor que no respeta las reglas de validación.
/// </summary>
public class ValidationException : DomainException
{
    /// <summary>
    /// El nombre del parámetro que falló la validación, si aplica.
    /// </summary>
    public string? Parameter {  get; init; } = null;

    /// <summary>
    /// El motivo por el que el valor es inválido, si aplica.
    /// </summary>
    public string? Reason {  get; init; } = null;

    /// <inheritdoc/>
    public ValidationException() : base("Un parámetro ingresado es inválido.") { }

    /// <summary>
    /// Crea una nueva instancia de este error con el nombre del parámetro que falló la validación.
    /// </summary>
    /// <param name="parameter">El nombre del parámetro que falló la validación.</param>
    public ValidationException(string parameter) : base($"El parámetro '{parameter}' es inválido.")
    {
        Parameter = parameter;
    }

    /// <summary>
    /// Crea una nueva instancia de este error con el nombre del parámetro y el motivo por el que falló la validación.
    /// </summary>
    /// <param name="parameter">El nombre del parámetro que falló la validación. <para>Tip: escribí <c>nameof(Parametro)</c> para que a futuro sea mas facil refactorizar.</para></param>
    /// <param name="reason">El motivo por el que falló la validación.</param>
    public ValidationException(string parameter, string reason) :
        base($"El parámetro '{parameter}' es inválido por el siguiente motivo: {reason}")
    {
        Parameter = parameter;
        Reason = reason;
    }
}
