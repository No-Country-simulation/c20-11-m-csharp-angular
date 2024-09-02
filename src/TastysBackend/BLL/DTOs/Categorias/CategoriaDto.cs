namespace Tastys.BLL;

public record CategoriaDto
{
    public required int CategoriaID { get; init; }

    public required string Nombre { get; init; }

    public required string ImgUrl { get; init; }

    public required int TotalRecetas { get; init; }
}
