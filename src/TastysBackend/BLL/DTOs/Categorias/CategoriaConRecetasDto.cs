namespace Tastys.BLL;

public record CategoriaConRecetasDto : CategoriaDto
{
    public RecetaDto[]? Recetas { get; set; }
}
