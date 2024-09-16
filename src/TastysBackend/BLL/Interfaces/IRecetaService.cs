using Tastys.BLL;
using Tastys.Domain;

public interface IRecetaService
{
    Task<List<RecetaDto>> GetAll();
    Task<List<RecetaDto>> GetAll(RecetasQuery queryParameters);
    Task<List<RecetaDto>> GetOrderRecetas(int pageIndex, int pageSize, QueryOrdersRecetas order, int day = -7);
    Task<RecetaDto> GetByID(int ID);
    Task<Receta> Create(Receta receta, List<string> list_c, List<IngredienteDto> list_ingredientes, string auth_id);
    Task<bool> UpdateById(RecetaDto receta, int ID);
    Task<bool> DeleteById(int ID);
}
