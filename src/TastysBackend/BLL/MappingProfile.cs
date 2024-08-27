using AutoMapper;
using Tastys.Domain;

namespace Tastys.BLL;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Receta, RecetaDto>();
        CreateMap<Usuario, UsuarioPublicDto>();
        CreateMap<Review, ReviewDto>();
        CreateMap<Categoria, CategoriaDto>();
    }
}
