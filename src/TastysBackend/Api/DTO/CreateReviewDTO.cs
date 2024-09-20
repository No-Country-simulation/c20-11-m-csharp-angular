using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Filters;

namespace DTO;
public class CreateReviewDTO
    {
        public int RecetaID { get; set; }
        public string? Comentario { get; set; }
        [Range(1, 5)]
        public int Calificacion { get; set; }
    }
public class ReviewRequestExample : IExamplesProvider<CreateReviewDTO>
{
    public CreateReviewDTO GetExamples()
    {
        return new CreateReviewDTO
        {
            RecetaID = 10,
            Comentario = "Esta receta es increíble, me encantó!",
            Calificacion = 5
        };
    }
}