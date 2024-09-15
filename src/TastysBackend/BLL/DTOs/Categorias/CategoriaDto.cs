namespace Tastys.BLL;

public record CategoriaDto
{
    public int CategoriaID { get; init; }

    public string Nombre { get; init; }

    public string ImgUrl { get; init; }

    public int TotalRecetas { get; set; }
}
