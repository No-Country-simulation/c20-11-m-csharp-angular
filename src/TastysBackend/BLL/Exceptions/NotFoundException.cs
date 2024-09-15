namespace Tastys.BLL;

/// <summary>
/// Lanzada cuando una entidad del dominio no se encontró.
/// </summary>
public class NotFoundException : DomainException
{
    /// <summary>
    /// La Id de la entidad buscada, si aplica.
    /// </summary>
    public int? Id { get; init; } = null;

    /// <summary>
    /// El tipo de la entidad buscada, si aplica.
    /// </summary>
    public Type? Type { get; init; } = null;

    /// <inheritdoc/>
    public NotFoundException() : base("No se encontró la entidad.") { }

    /// <inheritdoc/>
    public NotFoundException(string message) : base(message) { }

    /// <summary>
    /// Crea una nueva instancia de este error con el Id de la entidad que no se encontró.
    /// </summary>
    /// <param name="id">El Id de la entidad que no se encontró.</param>
    public NotFoundException(int id) : base($"No se encontró la entidad con el ID '{id}'.")
    {
        Id = id;
    }

    /// <summary>
    /// Crea una nueva instancia de este error con el Id y el tipo de la entidad que no se encontró.
    /// </summary>
    /// <param name="id">El Id de la entidad que no se encontró.</param>
    /// <param name="type">El tipo de la entidad buscada. Tip: escribí <c>typeof(Entidad)</c> para poder refactorizar luego.</param>
    public NotFoundException(Type type, int id) : base($"No se encontró la entidad '{type.Name}' con el ID '{id}'.")
    {
        Id = id;
        Type = type;
    }

    /// <summary>
    /// Crea una nueva instancia de este error con el Id de la entidad que no se encontró y un mensaje de error.
    /// </summary>
    /// <param name="id">El Id de la entidad que no se encontró.</param>
    /// <param name="message">Mensaje que describe el error.</param>
    public NotFoundException(int id, string message) : base(message)
    {
        Id = id;
    }

    /// <summary>
    /// Crea una nueva instancia de este error con el Id de la entidad que no se encontró y un mensaje de error.
    /// </summary>
    /// <param name="type">El tipo de la entidad buscada. Tip: escribí <c>typeof(Entidad)</c> para poder refactorizar luego.</param>
    /// <param name="id">El Id de la entidad que no se encontró.</param>
    /// <param name="message">Mensaje que describe el error.</param>
    public NotFoundException(Type type, int id, string message) : base(message)
    {
        Id = id;
        Type = type;
    }
}
