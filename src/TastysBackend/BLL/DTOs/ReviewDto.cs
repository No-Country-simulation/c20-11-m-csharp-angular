namespace Tastys.BLL;

public class ReviewDto
{
    public int ReviewID { get; init; }
    
    public required UsuarioPublicDto Usuario { get; init; }
    
    public required string Comentario { get; init; }
    
    public int Calificacion { get; init; }
    
    public bool IsDeleted { get; init; }
    
    public DateTime create_at { get; init; }  // DateTime encodeado como string, quiza necesite conversión
}
