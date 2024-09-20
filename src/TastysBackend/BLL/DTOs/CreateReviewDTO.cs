using System.ComponentModel.DataAnnotations;

public class CreateReviewDTO
{
    public int RecetaID { get; set; }
    public string? Comentario { get; set; }
    [Range(1, 5)]
    public int Calificacion { get; set; }
}