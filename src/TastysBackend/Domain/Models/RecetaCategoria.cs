namespace Domain
{
    public partial class RecetaCategoria
    {
        public int RecetaID { get; set; }
        public int CategoriaID { get; set; }

        public virtual Receta Receta { get; set; } = null!;
        public virtual Categoria Categoria { get; set; } = null!;
    }
}