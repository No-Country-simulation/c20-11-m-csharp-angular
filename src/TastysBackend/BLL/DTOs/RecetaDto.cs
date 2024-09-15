namespace Tastys.BLL;

public class RecetaDto
{
    public int RecetaID { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public string ImageUrl { get; set; }
    public UsuarioPublicDto Usuario { get; set; }
    public ICollection<ReviewDto> Reviews { get; set; }
    public ICollection<CategoriaDto> Categorias { get; set; }
}
