namespace Tastys.BLL;

/// <summary>
/// Lanzada cuando un usuario intenta realizar una acción o acceder a un recurso sin los permisos necesarios.
/// </summary>
public class UnauthorizedException : DomainException
{
    /// <summary>
    /// La acción o el recurso al que se intentó acceder, si aplica.
    /// </summary>
    public string? Target { get; init; } = null;

    /// <summary>
    /// El motivo por el que no se puede acceder, si aplica.
    /// </summary>
    public string? Reason { get; init; } = null;

    /// <inheritdoc/>
    public UnauthorizedException() : base("No tiene los permisos necesarios para realizar esta acción.") { }

    /// <summary>
    /// Crea una nueva instancia de este error con el nombre de la acción o recurso al que se intentó acceder.
    /// </summary>
    /// <param name="target">El nombre de la acción o recurso al que se intentó acceder.</param>
    public UnauthorizedException(string target) : base($"No tiene los permisos necesarios para acceder a '{target}'.")
    {
        Target = target;
    }

    /// <summary>
    /// Crea una nueva instancia de este error con el nombre de la acción o recurso y el motivo por el que no se puede acceder.
    /// </summary>
    /// <param name="target">El nombre de la acción o recurso al que se intentó acceder.</param>
    /// <param name="reason">El motivo por el que no se puede acceder.</param>
    public UnauthorizedException(string target, string reason) :
        base($"No tiene los permisos necesarios para acceder a '{target}' por el siguiente motivo: {reason}.")
    {
        Target = target;
        Reason = reason;
    }
}
