using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Tastys.BLL;

public class RecetaService
{
    private readonly IMapper _mapper;
    private readonly ITastysContext _context;

    public RecetaService(ITastysContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<RecetaDto> GetAllRecetas()
    {
        return _context.Recetas
            .Include(receta => receta.Usuario)
            .Select(receta => _mapper.Map<RecetaDto>(receta))
            .ToList();
    }

    public List<RecetaDto> GetAllRecetas(RecetasQuery queryParameters)
    {
        var query = _context.Recetas.Include(receta => receta.Usuario).AsQueryable();

        if (queryParameters.Offset.HasValue)
            query = query.Skip(queryParameters.Offset.Value);

        if (queryParameters.Length.HasValue)
            query = query.Take(queryParameters.Length.Value);

        // TODO: Falta la validación y también los filtros S y Review_Length

        return query
            .Select(receta => _mapper.Map<RecetaDto>(receta))
            .ToList();
    }
}
