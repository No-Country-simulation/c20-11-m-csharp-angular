using Tastys.BLL;
using Tastys.Domain;


    public interface IRecetaService
    {
        
        Task<List<RecetaDto>> GetAllRecetas();
        Task<List<RecetaDto>> GetOrderRecetas(int pageIndex, int pageSize, QueryOrdersRecetas order,int day = -7);
        Task<RecetaDto> GetRecetaByID(int ID);
        Task<bool> UpdateReceta(RecetaDto receta, int ID);//te devuelve bool si encontro o no la receta
        Task<Receta> CreateReceta(Receta receta, List<string> list_c, string auth_id);
    }

//usamos DTO para -en parte- esconder informacion sensible en caso de querer cuidarse con eso (y no mandamos el modelo completo)

