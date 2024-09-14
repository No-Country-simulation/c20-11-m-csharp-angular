using Swashbuckle.AspNetCore.Filters;

public class NewRecetaDTO
{
    public RecetaDtoDetailPost receta { get; set; }
    public List<string> list_c { get; set; }
    public int user_id { get; set; }
}

public class RecetaDtoDetailPost
{
    public string nombre { get; set; }
    public string descripcion { get; set; }
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
                imageUrl = "example.com"
            },
            list_c = new List<string> { "Postres" },
            user_id = 1
        };
    }
}