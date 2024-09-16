using Swashbuckle.AspNetCore.Filters;

public class NewRecetaDTO
{
    public RecetaDtoDetailPost receta { get; set; }
    public List<string> list_c { get; set; }
    public string? user_id { get; set; }
    public List<IngredienteDto> list_i { get; set; }
}

public class RecetaDtoDetailPost
{
    public string nombre { get; set; }
    public string descripcion { get; set; }
    public string tiempo_de_coccion { get; set; }
    public string imageUrl { get; set; }
}

public class RecetaRequestExample : IExamplesProvider<NewRecetaDTO>
{
    public NewRecetaDTO GetExamples()
    {
        return new NewRecetaDTO
        {
            receta = new RecetaDtoDetailPost
            {
                nombre = "ñoquis",
                descripcion = "alta receta",
                imageUrl = "example.com",
                tiempo_de_coccion = "30"
            },
            list_c = new List<string> { "Postres" },
            list_i = new List<IngredienteDto> { new IngredienteDto{Cantidad="5",Nombre="huevo"} },
            user_id = "1"
        };
    }
}