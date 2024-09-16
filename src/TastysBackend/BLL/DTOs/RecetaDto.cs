using Tastys.Domain;

namespace Tastys.BLL;

public class RecetaDto
    {
        public int RecetaID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ImageUrl { get; set; }
        public int UsuarioID { get; set; }
        public UsuarioPublicDto Usuario { get; set; }
        public ICollection<ReviewDto> Reviews { get; set; }
        public ICollection<CategoriaDto> Categorias { get; set; }
        public ICollection<IngredienteDto> Ingredientes { get; set; }
        public string? TiempoCoccion { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreateAt { get; set; } = DateTime.Now;
    }
