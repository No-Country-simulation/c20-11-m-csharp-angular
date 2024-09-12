using Tastys.BLL;
using Tastys.Domain;


    public interface IRecetaService
    {
        
        Task<List<RecetaDto>> GetAllRecetas();
        Task<RecetaDto> GetRecetaByID(int ID);
        Task<bool> UpdateReceta(RecetaDto receta, int ID);//te devuelve bool si encontro o no la receta
        Task<Receta> CreateReceta(Receta receta,List<string> list_c,int userId);
    }

//usamos DTO para -en parte- esconder informacion sensible en caso de querer cuidarse con eso (y no mandamos el modelo completo)

