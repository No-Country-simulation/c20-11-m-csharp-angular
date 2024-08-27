namespace Tastys.BLL;

public record RecetaDto
{
    public int RecetaID { get; init; }

    public required string Nombre { get; init; }

    public required string Descripcion { get; init; }

    public required string ImageUrl { get; init; }

    public required UsuarioPublicDto Usuario { get; init; }

    public required CategoriaDto[] Categorias { get; init; }

    public required ReviewDto[] Reviews { get; init; }

    public bool IsDeleted { get; init; }

    public DateTime? create_at { get; init; }
}
