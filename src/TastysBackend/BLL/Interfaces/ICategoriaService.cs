namespace Tastys.BLL;

public interface ICategoriaService
{
    Task<CategoriaConRecetasDto> GetCategoriaById(int id, CategoriaByIdQuery queryParameters);
    Task<List<CategoriaConRecetasDto>> GetCategorias(CategoriasQuery queryParameters);
}