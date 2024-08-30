namespace Tastys.BLL;

/// <summary>
/// Representa los datos de un usuario visibles por otros usuarios.
/// </summary>
public class UsuarioPublicDto
{
    public int UsuarioID { get; init; }
    
    public required string Nombre { get; init; }
}
