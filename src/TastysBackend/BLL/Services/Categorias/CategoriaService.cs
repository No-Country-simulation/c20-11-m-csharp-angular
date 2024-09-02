using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Tastys.Domain;

namespace Tastys.BLL;
public class CategoriaService
{
    private readonly IMapper _mapper;
    private readonly ITastysContext _context;

    public CategoriaService(ITastysContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Obtener todas las categorias, incluyendo sus recetas.
    /// </summary>
    public async Task<List<CategoriaConRecetasDto>> GetCategorias(CategoriasQuery queryParameters)
    {
        var query = _context.Categorias.AsQueryable();

        switch (queryParameters.OrdenPorCantRecetas)
        {
            case Ordering.Ascending:
                query = query.OrderBy(cat => cat.Recetas.Count);
                break;
            case Ordering.Descending:
                query = query.OrderByDescending(cat => cat.Recetas.Count);
                break;
        }

        var categorias = await query.Paginate(queryParameters).ToListAsync();
        var dtos = new List<CategoriaConRecetasDto>();

        foreach (var categoria in categorias)
        {
            var recetasQuery = _context.Recetas
                .Include(receta => receta.Categorias)
                .Where(receta => receta.Categorias.Contains(categoria));

            if (queryParameters.CantRecetas.HasValue)
            {
                recetasQuery = recetasQuery.Take(queryParameters.CantRecetas.Value);
            }

            var dto = _mapper.Map<CategoriaConRecetasDto>(categoria);
            dto.Recetas = await recetasQuery.Select(receta => _mapper.Map<RecetaDto>(receta)).ToArrayAsync();

            dtos.Add(dto);
        }

        return dtos;
    }

    /// <summary>
    /// Obtiene el detalle de una categoria con una lista de sus recetas posiblemente filtrada u ordenada.
    /// </summary>
    public async Task<CategoriaConRecetasDto> GetCategoriaById(int id, CategoriaByIdQuery queryParameters)
    {
        // No hago Include("Recetas"). Lo hago luego para poder limitar la cantidad
        var categoria = await _context.Categorias.FirstAsync(cat => cat.CategoriaID == id);

        var recetasQuery = _context.Recetas
            .Include(receta => receta.Categorias)
            .Where(receta => receta.Categorias.Contains(categoria))
            .Paginate(queryParameters);

        switch (queryParameters.OrdenarPorCantReviews)
        {
            case Ordering.Ascending:
                recetasQuery = recetasQuery.OrderBy(receta => receta.Reviews.Count);
                break;
            case Ordering.Descending:
                recetasQuery = recetasQuery.OrderByDescending(receta => receta.Reviews.Count);
                break;
        }

        switch (queryParameters.OrdenarPorFecha)
        {
            case Ordering.Ascending:
                recetasQuery = recetasQuery.OrderBy(receta => receta.create_at);
                break;
            case Ordering.Descending:
                recetasQuery = recetasQuery.OrderByDescending(receta => receta.create_at);
                break;
        }

        switch (queryParameters.OrdenarPorPuntuacion)
        {
            case Ordering.Ascending:
                recetasQuery = recetasQuery.OrderBy(receta => receta.Reviews.Average(rev => rev.Calificacion));
                break;
            case Ordering.Descending:
                recetasQuery = recetasQuery.OrderByDescending(receta => receta.Reviews.Average(rev => rev.Calificacion));
                break;
        }

        var categoriaDto = _mapper.Map<CategoriaConRecetasDto>(categoria);
        var recetasDtos = await recetasQuery.Select(receta => _mapper.Map<RecetaDto>(receta)).ToArrayAsync();

        categoriaDto.Recetas = recetasDtos;

        return categoriaDto;
    }
}
