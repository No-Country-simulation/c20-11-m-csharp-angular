using AutoMapper;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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
            .Include(receta => receta.Categorias)
            .Include(receta => receta.Reviews)
            .Select(receta => _mapper.Map<RecetaDto>(receta))
            .ToList();
    }

    public List<RecetaDto> GetAllRecetas(RecetasQuery queryParameters)
    {
        var query = _context.Recetas
            .Include(receta => receta.Usuario)
            .Include(receta => receta.Categorias)
            .AsQueryable();

        // TODO: Full-text search en nombre + descripción, pero probablemente requiera
        // hacer cambios en la DB para incluir un índice, porque si no es muy ineficiente
        if (!string.IsNullOrWhiteSpace(queryParameters.S))
        {
            query = query
                .Where(receta => receta.Nombre
                    .Contains(queryParameters.S, StringComparison.CurrentCultureIgnoreCase));
        }

        if (queryParameters.Offset.HasValue)
        {
            query = query.Skip(queryParameters.Offset.Value);
        }

        if (queryParameters.Length.HasValue)
            query = query.Take(queryParameters.Length.Value);

        // TODO: Falta filtro Reviews_Length

        return query
            .Select(receta => _mapper.Map<RecetaDto>(receta))
            .ToList();
    }
}
