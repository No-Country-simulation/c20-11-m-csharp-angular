using Tastys.Domain;

namespace Tastys.BLL;

public interface ICategoriaService
{
    Task<CategoriaConRecetasDto> GetCategoriaById(int id, CategoriaByIdQuery queryParameters);
    Task<List<CategoriaConRecetasDto>> GetCategorias(CategoriasQuery queryParameters);
    List<Categoria> GetCategorias(int pageIndex,int pageSize);
}